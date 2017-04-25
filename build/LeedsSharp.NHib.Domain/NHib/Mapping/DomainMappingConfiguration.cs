using System;
using FluentNHibernate.Automapping;

namespace LeedsSharp.NHib.Domain.NHib.Mapping
{
	public class DomainMappingConfiguration : DefaultAutomappingConfiguration
	{
		public override bool IsComponent(Type type)
		{
			return type.Namespace != null && type.Namespace.StartsWith("LeedsSharp.NHib.Domain.Models.ValueTypes");
		}

		public override bool ShouldMap(Type type)
		{
			return type.Namespace != null && type.Namespace.StartsWith("LeedsSharp.NHib.Domain.Models.Entities");
		}

	}
}