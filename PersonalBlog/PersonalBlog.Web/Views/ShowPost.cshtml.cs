using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalBlog.Core.Entites;

namespace PersonalBlog.Web.Views
{
    public class ShowPost : PageModel
    {
        public ShowPost()
        {
            
        }
        public Post Post { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}