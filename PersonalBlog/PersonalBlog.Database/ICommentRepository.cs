using System;
using System.Collections.Generic;
using PersonalBlog.Core.Entites;

namespace PersonalBlog.Database
{
	public interface ICommentRepository
	{
		bool Add(Guid userId, Guid postId, string comment);
		IEnumerable<Comment> Get(Guid postId);
	}
}