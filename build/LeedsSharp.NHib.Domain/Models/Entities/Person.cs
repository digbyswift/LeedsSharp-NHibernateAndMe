using System;
using System.Collections.Generic;
using LeedsSharp.NHib.Domain.Models.ValueTypes;

namespace LeedsSharp.NHib.Domain.Models.Entities
{
	public class Person : EntityBase<Guid, Person>
	{
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual DateTime DateCreated { get; set; }
		public virtual DateTime? LastUpdated { get; set; }

		public virtual Address Address { get; set; }

		public virtual IList<Server> PermittedServers { get; set; }
		public virtual ISet<Login> LoginHistory { get; set; }

		public Person()
		{
			DateCreated = DateTime.UtcNow;

			PermittedServers = new List<Server>();
			LoginHistory = new HashSet<Login>();
		}
	}

}