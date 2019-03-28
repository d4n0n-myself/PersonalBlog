using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PersonalBlog.Web.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        public Exception Exception { get; set; }
//        public string RequestId { get; set; }
//
//        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
//
//        public void OnGet()
//        {
//            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
//        }
    }
}
