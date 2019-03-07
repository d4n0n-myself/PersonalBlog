using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApplication1.Controllers;

namespace WebApplication1
{
	/// <summary>
	/// This class provides intermediate support.
	/// </summary>
	internal class Helpers
	{
		internal static Task LoadPageInResponse(HttpContext context, string pageName)
		{
			var page = File.ReadAllText($"Views/{pageName}.html");
			using (var streamWriter = new StreamWriter(context.Response.Body))
				return streamWriter.WriteAsync(page);
		}

		internal static string GetPosts(HttpContext context)
		{
			using (var controller = new PostsController())
			{
				var posts = controller.Get(context);

				var sb = new StringBuilder();
				sb.Append(@"<ul style=""padding-left: 50px; padding-top: 15px;"">");
				foreach (var post in posts)
					sb.Append($@"<li>Author: {post.UserId}><b>Title: {post.Header}</b><br><pre>{post.Body}</pre></li>");
				sb.Append("</ul>");
				return sb.ToString();
			}
		}
	}
}