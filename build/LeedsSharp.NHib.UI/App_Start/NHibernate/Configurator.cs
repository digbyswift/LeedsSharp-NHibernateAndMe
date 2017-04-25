using System;
using System.Configuration;
using System.Reflection;
using System.Web.Hosting;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using FYI.BriefingPortal.UI.NHibernate;
using LeedsSharp.NHib.Domain.Models.Entities;
using LeedsSharp.NHib.Domain.NHib.Events;
using LeedsSharp.NHib.Domain.NHib.Mapping;
using NHibernate;
using NHibernate.Event;

namespace LeedsSharp.NHib.UI.NHibernate
{
    public static class Configurator
	{

		public static ISessionFactory GetSessionFactory()
        {
	        var config = ConfigHelper.TryLoadConfiguration();
	        if (config == null)
	        {
		        config = Fluently
			        .Configure()
			        .Database(DatabaseConfig())
			        .Mappings(MappingConfig())
			        .ExposeConfiguration(cfg =>
			        {
				        cfg.AppendListeners(ListenerType.PreUpdate, new[] { new DateAuditEventListener() });
			        })
			        .BuildConfiguration();

		        ConfigHelper.PersistConfiguration(config);
	        }

	        return config.BuildSessionFactory();
        }


	    private static MsSqlConfiguration DatabaseConfig()
	    {
		    return MsSqlConfiguration.MsSql2012
			    .ConnectionString(ConfigurationManager.ConnectionStrings["Default"].ConnectionString)
			    .AdoNetBatchSize(20);
	    }

	    private static Action<MappingConfiguration> MappingConfig()
	    {
		    var domainAssembly = Assembly.GetAssembly(typeof(Person));

		    return m => m
				    .AutoMappings
				    .Add(AutoMap
					    .Assembly(domainAssembly, new DomainMappingConfiguration())
					    .UseOverridesFromAssembly(domainAssembly)
						
					    .Conventions.Setup(c =>
					    {
							c.Add(DefaultCascade.All());
							c.Add<CustomForeignKeyConvention>();
							c.Add<DefaultStringLengthConvention>();
							c.Add<EnumConvention>();
							c.Add<DateTimeConvention>();
					    }))
				    .ExportTo(HostingEnvironment.MapPath("~/App_Data/Mappings/"));
	    }
	}
}