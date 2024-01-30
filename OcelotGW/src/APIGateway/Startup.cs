using APIGateway.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.Text;
using Microsoft.Identity.Web;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.JsonWebTokens;



namespace APIGateway
{

    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IConfiguration OcelotConfiguration { get; }

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath)
                   .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
                   .AddEnvironmentVariables();

            OcelotConfiguration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //var jwtOptions = Configuration
            //.GetSection("JwtOptions")
            //.Get<JwtOptions>();

            //services.AddAuthentication().AddJwtBearer("jwt", options =>
            //{
            //    //convert the string signing key to byte array
            //    byte[] signingKeyBytes = Encoding.ASCII
            //        .GetBytes(jwtOptions.SigningKey);


            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = jwtOptions.Issuer,
            //        ValidAudience = jwtOptions.Audience,
            //         IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
            //        //SignatureValidator = delegate (string token, TokenValidationParameters parameters)
            //        //{
            //        //    var jwt = new JwtSecurityToken(token);

            //        //    return jwt;
            //        //},
            //    };
            //});

            var jwtOptionsEntra = Configuration
            //.GetSection("JwtOptions")
            .GetSection("EntraId")
            .Get<JwtOptionEntraId>();

            //services.AddAuthentication("jwt")
            //.AddMicrosoftIdentityWebApi(Configuration.GetSection("EntraId"));



            //services.Configure<JwtBearerOptions>("jwt", options =>
            //{
            //    options.TokenValidationParameters.ValidateIssuer = false; // accept several tenants (here simplified)
            //    //options.Events = new JwtBearerEvents
            //    //{
            //    //    OnAuthenticationFailed = AuthenticationFailed
            //    //};

            //    options.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidateLifetime = true,
            //        ValidateAudience = true,
            //        ValidAudience = jwtOptionsEntra.Audience
            //    };
            //});



            services
            .AddAuthentication("jwt")
            .AddJwtBearer("jwt", options =>
            {
                byte[] signingKeyBytes = Encoding.UTF8
                    .GetBytes(jwtOptionsEntra.Kid);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = false,
                    SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                    {
                        var jwt = new JsonWebToken(token);

                        return jwt;
                    },
                    ValidIssuer = jwtOptionsEntra.Instance,
                    ValidAudience = jwtOptionsEntra.Audience,
                   // IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)

                };
            });
            //.AddMicrosoftIdentityWebApi(
            //        o => Configuration.Bind("EntraId", o),
            //        o => Configuration.Bind("EntraId", o));
            services.AddOcelot(OcelotConfiguration);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }       

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOcelot().Wait();
        }
    }
}
