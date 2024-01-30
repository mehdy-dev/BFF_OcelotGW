using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FE.Endpoint.Pages
{
    public class ResultModel : PageModel
    {
        public void OnGet()
        {

            string result = Request.Query["param2"];
            if (result == null)
            {

            }
            else
            {

            }






        }
    }
}
