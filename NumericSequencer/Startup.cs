using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web.WebApi;
using Ninject.Web.WebApi.OwinHost;
using Ninject.Web.Common.OwinHost;
using Owin;
using System.Linq;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles.ContentTypes;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.IO;

[assembly: OwinStartup(typeof(NumericSequencer.Startup))]

namespace NumericSequencer
{
	public partial class Startup
	{
		static Startup()
		{
			BaseDirectory = ".";
		}

		internal static string BaseDirectory { get; set; }

		public void Configuration(IAppBuilder app)
		{
			var webApiConfiguration = new HttpConfiguration();
			webApiConfiguration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			webApiConfiguration.MapHttpAttributeRoutes();

			app
				.UseNinjectMiddleware(CreateKernel)
				.UseNinjectWebApi(webApiConfiguration)
				.UseFileServer(new FileServerOptions
				{
					EnableDefaultFiles = false,
					EnableDirectoryBrowsing = false,
					StaticFileOptions =
					{
						RequestPath = new PathString("/css"),
						FileSystem = new PhysicalFileSystem(BaseDirectory + "\\content"),
						ContentTypeProvider = new FileExtensionContentTypeProvider(
							new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
							{
								{".css", "text/css"}
							})
					}
				})
				.UseFileServer(new FileServerOptions
				{
					EnableDefaultFiles = false,
					EnableDirectoryBrowsing = false,
					StaticFileOptions =
					{
						RequestPath = new PathString("/js"),
						FileSystem = new PhysicalFileSystem(BaseDirectory + "\\scripts"),
						ContentTypeProvider = new FileExtensionContentTypeProvider(
							new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
							{
								{".js", "application/javascript"}
							})
					}
				})
				.UseFileServer(new FileServerOptions
				{
					EnableDefaultFiles = true,
					DefaultFilesOptions =
					{
						DefaultFileNames = { "index.html" }
					},
					EnableDirectoryBrowsing = false,
					StaticFileOptions =
					{
						RequestPath = new PathString(""),
						FileSystem = new PhysicalFileSystem(BaseDirectory + "\\static"),
						ContentTypeProvider = new FileExtensionContentTypeProvider(
							new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
							{
								{".html", "text/html"}
							})
					}
				});
		}

		static IKernel CreateKernel()
		{
			var kernel = new StandardKernel();
			RegisterServices(kernel);
			return kernel;
		}

		static void RegisterServices(IKernel kernel)
		{
			kernel.Bind(x => x.FromThisAssembly()
				.SelectAllClasses()
				.BindDefaultInterface()
				.Configure(cfg => cfg.InTransientScope()));
		}
	}
}
