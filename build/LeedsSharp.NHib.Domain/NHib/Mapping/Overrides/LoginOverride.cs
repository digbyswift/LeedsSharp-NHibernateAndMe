using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using LeedsSharp.NHib.Domain.Models.Entities;
using LeedsSharp.NHib.Domain.NHib.UserTypes;

namespace LeedsSharp.NHib.Domain.NHib.Mapping.Overrides
{
	public class LoginOverride : IAutoMappingOverride<Login>
	{
		public void Override(AutoMapping<Login> mapping)
		{
			mapping.Id(x => x.Id, "LoginId");

			mapping.Map(x => x.DateLogged, "DateLoggedIn");

			mapping
				.Map(x => x.Ip)
				.CustomType<IpUserType>();
		}
	}

}