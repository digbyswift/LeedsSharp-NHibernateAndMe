using System;
using System.Net;

namespace LeedsSharp.NHib.Domain.Models.Entities
{
	public class Login : EntityBase<Guid, Login>
	{
		public virtual IPAddress Ip { get; set; }
		public virtual DateTime DateLogged { get; set; }

		public virtual Person Person { get; set; }

		public Login()
		{
			DateLogged = DateTime.UtcNow;
		}
	}

}