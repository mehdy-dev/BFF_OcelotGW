using System;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FE.Endpoint.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public string accessToken { get; set; } 
   
        public IActionResult OnGet()
        {

            string result =  Request.Query["param1"];
             accessToken = Request.Query["param2"];
            if (result == null)
            {

            }
            else
            {
                ViewData["user"] = result;
               ViewData["accessToken"] = accessToken;
            }



           //  new RedirectToPageResult("Result", new { param2 = jwt }); 

            //HACK ridiculous i have to do all this instead of just getting the cookie from the cookies collection
              var cookieFromHeaderString = (HttpContext.Request.Headers["Cookie"]).FirstOrDefault();

            //if (cookieFromHeaderString != null)
            //{

            //    string[] strArray = cookieFromHeaderString.Split(new string[] { "; " }, StringSplitOptions.None);
            //    string BFFCookie = strArray.Where(m => m.StartsWith("test=")).FirstOrDefault();

            //    if (BFFCookie != null)
            //    {
            //        //int start = whCookie.IndexOf("=") + 1;
            //        //string cookieValue = whCookie.Substring(start);

            //        //string[] whArray = cookieValue.Split(' ');

            //        //int viewportWidth = 0;
            //        //int viewportHeight = 0;

            //        //if (whArray.Length == 2)
            //        //{
            //        //    int.TryParse(whArray[0], out viewportWidth);
            //        //    int.TryParse(whArray[1], out viewportHeight);
            //        //}
            //    }
            //}

            return new PageResult();

        }

        public  IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return new PageResult();
            }
            var ResultAPICall = OnGetAccessToAPI(Request.Query["param2"]);
            ResultAPICall.Wait();
            // otherwise do some processing
            ViewData["res"] = ResultAPICall.Result.Value.ToString();
            return new PageResult();

        }

        public async Task<JsonResult> OnGetAccessToAPI(string token)
        {
            string apiResponse;
           // List<IncidentModel> incidents = new List<IncidentModel>();
            using (var httpClient = new HttpClient())


            {

                var requestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("http://localhost:5000/apigateway/categories")
                };


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                using (var response = await httpClient.SendAsync(requestMessage))
                {


                    apiResponse = await response.Content.ReadAsStringAsync();
                    //var res = JsonConvert.DeserializeObject<List<IncidentModel>>(apiResponse);
                }
                
            }
            return new JsonResult(apiResponse);
        }





    }
}
