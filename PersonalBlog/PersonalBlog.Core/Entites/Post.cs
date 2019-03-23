using System;

namespace PersonalBlog.Core.Entites
{
    public class Post
    {
        public Post(Guid userId, string header, string content, string body)
        {
            UserId = userId;
            Header = header;
            Content = content;
            Body = body;
            Id = Guid.NewGuid();
        }

        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public string Body { get; set; }
    }
}