using System.Configuration;
using System.Reflection;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using LeedsSharp.NHib.Domain.Models.Entities;
using NHibernate;

namespace LeedsSharp.NHib.UI.NHibernate
{
    public static class ConfiguratorMin1
	{

		public static ISessionFactory GetSessionFactory()
        {
	        var domainAssembly = Assembly.GetAssembly(typeof(Person));
	        var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

	        return Fluently
		        .Configure()
		        .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
		        .Mappings(m => m.AutoMappings.Add(AutoMap.Assembly(domainAssembly)))
		        .BuildConfiguration()
		        .BuildSessionFactory();
        }

	}
}