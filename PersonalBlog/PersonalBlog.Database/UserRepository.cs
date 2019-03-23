using System;
using System.Linq;
using PersonalBlog.Core.Entites;

namespace PersonalBlog.Database
{
	public class UserRepository
	{
		public UserRepository(ApplicationContext context)
		{
			_context = context;
		}

		public UserRepository()
		{
			_context = new ApplicationContext();
		}

		public void AddImg(Guid userId, string imgLink)
		{
			var user = _context.Users.FirstOrDefault(u => u.Id == userId) ??
			           throw new ArgumentException("No such user in database!");
			user.ImgLink = imgLink;
			_context.Users.Update(user);
			_context.SaveChanges();
		}

		public void AddUser(string login, string password)
		{
			if (!_context.Users.Any(u => u.Login == login))
				_context.Users.Add(new User(login, password, null));
			_context.SaveChanges();
		}

		public bool CheckPassword(string login, string password) =>
			_context.Users.First(u => u.Login == login).Password.Equals(password);

		public bool ContainUser(string login) => _context.Users.Any(u => u.Login.Equals(login));

		public User GetUserByLogin(string login) => _context.Users.FirstOrDefault(u => u.Login == login);

		public string GetImgLink(Guid userId) => _context.Users.FirstOrDefault(u => u.Id == userId)?.ImgLink;

		private readonly ApplicationContext _context;
	}
}