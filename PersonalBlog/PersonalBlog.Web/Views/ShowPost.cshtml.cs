using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalBlog.Core.Entites;
using PersonalBlog.Database;

namespace PersonalBlog.Web.Views
{
    public class ShowPost : PageModel
    {
        public ShowPost()
        {
        }

        public Post Post { get; set; }
        public IEnumerable<Comment> Comments { get; set; }

        private readonly UserRepository _userRepository = new UserRepository();

        public string GetUserLogin(Guid id) => _userRepository.GetUserById(id).Login;
    }
}