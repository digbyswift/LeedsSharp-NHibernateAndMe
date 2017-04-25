using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using LeedsSharp.NHib.UI.Autofac;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LeedsSharp.NHib.UI
{
    public class Global : HttpApplication
	{
		private IContainer _container;

		public void Application_OnStart()
		{
			ConfigureAutofac();
			ConfigureMvc();
		}

		private void ConfigureAutofac()
		{
			var builder = new ContainerBuilder();

			builder.RegisterControllers(Assembly.GetExecutingAssembly());
			builder.RegisterFilterProvider();
			builder.RegisterModule(new NHibernateModule());
			builder.RegisterModule(new DomainServicesModule());

			_container = builder.Build();
		}

		private void ConfigureMvc()
		{
			DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			JsonConvert.DefaultSettings = () => new JsonSerializerSettings
			{
				Formatting = Formatting.None,
				NullValueHandling = NullValueHandling.Ignore,
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			};
		}

	}
}
