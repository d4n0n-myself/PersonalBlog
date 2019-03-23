using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Web.Pages;

namespace WebApplication1
{
	/// <summary>
	/// Used to authentificate users in system.
	/// </summary>
	public class AuthentificationMiddleware
	{
		public AuthentificationMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			if (!context.Request.Cookies.ContainsKey("userLogin") &&
			    !context.Request.Path.StartsWithSegments(new PathString("/Home/Login")))
				context.Response.Redirect("/Home/Login");
			if (context.Request.Cookies.ContainsKey("userLogin") &&
			    context.Request.Path.StartsWithSegments(new PathString("/Home/Login")))
				context.Response.Redirect("/");

			await _next.Invoke(context);
		}

		private readonly RequestDelegate _next;
	}
}