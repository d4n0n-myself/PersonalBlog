using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PersonalBlog.Web.Pages
{
    public class Login : PageModel
    {
        public string PasswordWrong { get; set; } = "";
    }
}