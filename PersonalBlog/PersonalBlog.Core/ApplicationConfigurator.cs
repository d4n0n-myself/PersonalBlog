using Microsoft.Extensions.Configuration;

namespace PersonalBlog.Core
{
	public class ApplicationConfigurator
	{
		public IConfigurationRoot ConfigurationRoot { get; set; }

		public ApplicationConfigurator()
		{
			ConfigurationRoot = new ConfigurationBuilder().AddJsonFile("appsettings.json")
				.Build();
		}
	}
}