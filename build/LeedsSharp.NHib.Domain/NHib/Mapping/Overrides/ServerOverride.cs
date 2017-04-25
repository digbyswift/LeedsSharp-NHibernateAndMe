using System.Collections.Generic;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using LeedsSharp.NHib.Domain.Models.Entities;
using LeedsSharp.NHib.Domain.NHib.UserTypes;

namespace LeedsSharp.NHib.Domain.NHib.Mapping.Overrides
{
	public class ServerOverride : IAutoMappingOverride<Server>
	{
		public void Override(AutoMapping<Server> mapping)
		{
			mapping
				.Map(x => x.IpWhitelist)
				.CustomType<JsonUserType<IEnumerable<string>>>();

			mapping
				.HasManyToMany(x => x.AssignedPeople);
		}
	}

}