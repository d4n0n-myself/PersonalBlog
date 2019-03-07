using Microsoft.AspNetCore.Mvc;
using WebApplication1.Database;
using WebApplication1.Database.Entites;

namespace WebApplication1.Controllers
{
	/// /// <summary>
	///	This class collects information about users.
	/// </summary>
	public class UsersController : Controller
	{
		public void Add(string login, string password) => Repo.AddUser(login, password);

		public bool Contains(string login) => Repo.ContainUser(login);

		public bool Check(string login, string password) => Repo.CheckPassword(login, password);

		public User Get(string login) => Repo.GetUserByLogin(login);

		private static readonly UserRepository Repo = new UserRepository();
	}
}