using System;

namespace PersonalBlog.Core.Entites
{
    public class Comment
    {
        public Comment(string commentText, Guid userId, Guid postId)
        {
            Id = Guid.NewGuid();
            CommentText = commentText;
            UserId = userId;
            PostId = postId;
        }

        public Guid Id { get; set; }
        public string CommentText { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
    }
}