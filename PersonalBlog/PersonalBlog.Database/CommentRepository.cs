using System;
using System.Collections.Generic;
using System.Linq;
using PersonalBlog.Core.Entites;

namespace PersonalBlog.Database
{
    public class CommentRepository: ICommentRepository
    {
        public CommentRepository(ApplicationContext context)
        {
            _context = context;
        }
        
        public CommentRepository()
        {
            _context = new ApplicationContext();
        }

        public bool Add(Guid userId, Guid postId, string comment)
        {
            try
            {
                _context.Comments.Add(new Comment(comment, userId, postId));
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Comment> Get(Guid postId)=>
            _context.Comments.Where(c => c.PostId.Equals(postId)).ToList();
        
        private readonly ApplicationContext _context;
        
    }
}