using Autofac;
using LeedsSharp.NHib.Domain.NHib.Data;
using Module = Autofac.Module;

namespace LeedsSharp.NHib.UI.Autofac
{
	public class DomainServicesModule : Module
	{
		protected override void Load(ContainerBuilder builder)
        {
			// Per request
			builder.RegisterType<TransactionFactory>().AsSelf().InstancePerRequest();

			//// Per dependency
			var domainAssembly = typeof(TransactionFactory).Assembly;

			builder.RegisterAssemblyTypes(domainAssembly)
				.Where(t => t.Name.EndsWith("Service"))
				.AsImplementedInterfaces();
		}

	}
}