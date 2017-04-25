using System;
using System.Collections.Generic;

namespace LeedsSharp.NHib.Domain.Models.Entities
{
	public class Server : EntityBase<Guid, Server>
	{
		public virtual string Name { get; set; }
		public virtual IEnumerable<string> IpWhitelist { get; set; }

		public virtual IEnumerable<Person> AssignedPeople { get; set; }
	}

}