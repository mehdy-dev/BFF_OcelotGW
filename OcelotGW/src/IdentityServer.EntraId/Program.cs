using System.Security.Claims;
using OidcProxy.Net.EntraId;
using OidcProxy.Net;
using Host;
using StackExchange.Redis;
using static IdentityModel.OidcConstants;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.Net.Http.Headers;
using System.Net.Http;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json.Linq;
var builder = WebApplication.CreateBuilder(args);

//var redisConnectionString = builder.Configuration.GetSection("ConnectionStrings:Redis").Get<string>();
//var connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);

var entraIdConfig = builder.Configuration
    .GetSection("OidcProxy")
    .Get<EntraIdProxyConfig>();

builder.Services.AddEntraIdProxy(entraIdConfig, o =>
{
    o.AddClaimsTransformation<MyClaimsTransformation>();
    o.LoadYarpFromConfig(builder.Configuration.GetSection("ReverseProxy"));
    //o.ConfigureRedisBackBone(connectionMultiplexer);
});



var app = builder.Build();



app.MapGet("/custom/me", async context =>
    {
        var identity = (ClaimsIdentity)context.User.Identity;



        if (string.IsNullOrEmpty(identity.Name))
        {
            context.Response.StatusCode = 401;
            return;
        }

        // var redirectResult = Results.Redirect("https://localhost:7049/index");

        var url = "https://localhost:7049/index";


        var queryString = HttpUtility.ParseQueryString(string.Empty);


        var subject = new
        {
            Sub = identity.Name,
            Claims = identity.Claims.Select(x => new
            {
                Type = x.Type,
                Value = x.Value
            })
        };

        queryString["param1"] = subject.Claims.Select(x => $"{x.Value}").Where(v => v.Contains("@")).FirstOrDefault();
        //queryString["param2"] = identity.Claims.Select(x => x.Subject.Name).First();
       // queryString["param3"] = identity.Claims.Select(x => x.).First();

        url += "?" + queryString;

        var resp = new HttpResponseMessage();

        var cookie = new CookieHeaderValue("session-id", "12345");

        CookieOptions option = new CookieOptions();
        option.Expires = DateTime.Now.AddMilliseconds(10);
        context.Response.Cookies.Append("testKey", "testValue", option);


        context.Response.Redirect(url);

        //await context.Response.WriteAsJsonAsync(new
        //{
        //    Sub = identity.Name,
        //    Claims = identity.Claims.Select(x => new
        //    {
        //        Type = x.Type,
        //        Value = x.Value
        //    })
        //});
    })
    .RequireAuthorization();

app.UseHttpsRedirection();

app.UseEntraIdProxy();

app.MapFallbackToFile("index.html");

app.Run();
 