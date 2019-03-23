using System;
using System.Collections.Generic;
using System.Linq;
using PersonalBlog.Core.Entites;

namespace PersonalBlog.Database
{
    public class CommentRepository
    {
        public CommentRepository(ApplicationContext context)
        {
            _context = context;
        }
        
        public CommentRepository()
        {
            _context = new ApplicationContext();
        }

        public void Add(string commentText, Guid userId, Guid postId) =>
            _context.Comments.Add(new Comment(commentText, userId, postId));

        public List<Comment> GetPostComments(Guid postId) =>
            _context.Comments.Where(c => c.PostId.Equals(postId)).ToList();
        
        private readonly ApplicationContext _context;
    }
}