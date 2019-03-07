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

		public void AddUser(string login, string password)
		{
			if (!_context.Users.Any(u => u.Login == login))
				_context.Users.Add(new User(login, password));
			_context.SaveChanges();
		}

		public bool ContainUser(string login) => _context.Users.Any(u => u.Login.Equals(login));

		public User GetUserByLogin(string login) => _context.Users.First(u => u.Login == login);
		
		public bool CheckPassword(string login, string password) => _context.Users.First(u => u.Login == login).Password.Equals(password);

		private readonly ApplicationContext _context;
	}
}