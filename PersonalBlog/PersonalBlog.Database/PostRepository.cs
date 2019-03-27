using System;
using System.Collections.Generic;
using System.Linq;
using PersonalBlog.Core.Entites;

namespace PersonalBlog.Database
{
    public class PostRepository
    {
        public PostRepository(ApplicationContext context)
        {
            _context = context;
        }

        public PostRepository()
        {
            _context = new ApplicationContext();
        }

        public void AddPost(string header, string content, string body, Guid userId)
        {
            if (_context.Posts.Any(p => p.Header == header)) throw new ArgumentException("Name already exists!");
            _context.Posts.Add(new Post(userId, header, content, body));
            _context.SaveChanges();
        }

        public bool ContainPost(string header) => _context.Posts.Any(p => p.Header.Equals(header));

        public Post GetPostByHeader(string header) => _context.Posts.First(p => p.Header.Equals(header));

        public List<Post> GetUsersPosts(Guid userId) => _context.Posts.Where(p => p.UserId.Equals(userId)).ToList();

        public List<Post> GetAllPosts() => _context.Posts.ToList();

        private readonly ApplicationContext _context;
    }
}