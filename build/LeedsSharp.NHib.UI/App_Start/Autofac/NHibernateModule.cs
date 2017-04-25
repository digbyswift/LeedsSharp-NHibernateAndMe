using Autofac;
using NHibernate;
using LeedsSharp.NHib.UI.NHibernate;

namespace LeedsSharp.NHib.UI.Autofac
{
    public class NHibernateModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
			var sessionFactory = Configurator.GetSessionFactory();

			builder.RegisterInstance(sessionFactory);

			builder
				.Register(c => c.Resolve<ISessionFactory>().OpenSession())
				.As<ISession>()
				.InstancePerRequest()
				.OnRelease(x => x.Dispose()
			);

		    builder
			    .Register(c => c.Resolve<ISessionFactory>().OpenStatelessSession())
			    .As<IStatelessSession>()
			    .InstancePerRequest()
			    .OnRelease(x => x.Dispose()
			);
		}
    }
}