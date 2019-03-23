using System;
using PersonalBlog.Database;

namespace PersonalBlog.Web
{
	public static class Helper
	{
		public static Guid GetUserId(string userLogin)
		{
			var userRepo = new UserRepository();
			return userRepo.GetUserByLogin(userLogin).Id;
		}
	}
}