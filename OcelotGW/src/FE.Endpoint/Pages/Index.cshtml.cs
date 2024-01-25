using System;
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

        public void OnGet()
        {

            string result =  Request.Query["param1"];
            if (result == null)
            {

            }
            else
            {
                ViewData["user"] = result;
            }


            //HACK ridiculous i have to do all this instead of just getting the cookie from the cookies collection
            var cookieFromHeaderString = (HttpContext.Request.Headers["Cookie"]).FirstOrDefault();

            if (cookieFromHeaderString != null)
            {

                string[] strArray = cookieFromHeaderString.Split(new string[] { "; " }, StringSplitOptions.None);
                string BFFCookie = strArray.Where(m => m.StartsWith("test=")).FirstOrDefault();

                if (BFFCookie != null)
                {
                    //int start = whCookie.IndexOf("=") + 1;
                    //string cookieValue = whCookie.Substring(start);

                    //string[] whArray = cookieValue.Split(' ');

                    //int viewportWidth = 0;
                    //int viewportHeight = 0;

                    //if (whArray.Length == 2)
                    //{
                    //    int.TryParse(whArray[0], out viewportWidth);
                    //    int.TryParse(whArray[1], out viewportHeight);
                    //}
                }
            }

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return new PageResult();
            }
            // otherwise do some processing
            return new RedirectToPageResult("Result");
        }

        static HttpClient myAppHTTPClient = new HttpClient();

    }
}
