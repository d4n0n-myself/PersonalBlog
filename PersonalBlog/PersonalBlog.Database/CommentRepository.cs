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

        public Comment Add(Guid userId, Guid postId, string comment)
        {
            try
            {
                var entityEntry = _context.Comments.Add(new Comment(comment, userId, postId));
                _context.SaveChanges();
                return entityEntry.Entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public IEnumerable<Comment> Get(Guid postId)=>
            _context.Comments.Where(c => c.PostId.Equals(postId)).ToList();
        
        private readonly ApplicationContext _context;
        
    }
}