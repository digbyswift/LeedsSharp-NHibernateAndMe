using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using LeedsSharp.NHib.Domain.Models.Entities;

namespace LeedsSharp.NHib.Domain.NHib.Mapping.Overrides
{
	public class PersonOverride : IAutoMappingOverride<Person>
	{
		public void Override(AutoMapping<Person> mapping)
		{
			mapping
				.Component(x => x.Address, x =>
				{
					x.Map(a => a.Line1).Column("AddressLine1");
					x.Map(a => a.Line2).Column("AddressLine2");
					x.Map(a => a.TownCity);
					x.Map(a => a.County);
					x.Map(a => a.PostCode);
				});

			mapping
				.HasMany(x => x.LoginHistory)
				.Inverse();

			mapping
				.HasManyToMany(x => x.PermittedServers)
				.Table("PersonsToServers")
				.Inverse();
		}
	}

}