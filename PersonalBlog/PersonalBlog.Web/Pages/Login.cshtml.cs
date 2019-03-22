using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PersonalBlog.Web.Pages
{
    public class Login : PageModel
    {    
        public void onGet()
        {
            
        }
        public bool PasswordWrong { get; set; }
    }
}