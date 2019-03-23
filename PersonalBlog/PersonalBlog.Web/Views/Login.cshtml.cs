using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PersonalBlog.Web.Views
{
    public class Login : PageModel
    {
        public string PasswordWrong { get; set; } = "";
    }
}