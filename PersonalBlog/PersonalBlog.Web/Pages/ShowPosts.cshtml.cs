using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalBlog.Core.Entites;

namespace PersonalBlog.Web.Pages
{
    public class ShowPosts : PageModel
    {
        public IEnumerable<Post> List { get; set; }
    }
}