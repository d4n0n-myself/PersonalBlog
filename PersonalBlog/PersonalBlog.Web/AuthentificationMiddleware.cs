using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
			    !context.Request.Path.StartsWithSegments(new PathString("/Login")))
				context.Response.Redirect("/Login");
			if (context.Request.Cookies.ContainsKey("userLogin") &&
			    context.Request.Path.StartsWithSegments(new PathString("/Login")))
				context.Response.Redirect("/");
			await _next.Invoke(context);
		}

		private readonly RequestDelegate _next;
	}
}