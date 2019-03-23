using System;

namespace PersonalBlog.Core.Entites
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        
        public string ImgLink { get; set; }

        public User(string login, string password, string imgLink)
        {
            Id = Guid.NewGuid();
            Login = login;
            Password = password;
            ImgLink = imgLink;
        }
    }
}