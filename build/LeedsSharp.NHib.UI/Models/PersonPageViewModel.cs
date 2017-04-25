using System.Collections.Generic;
using LeedsSharp.NHib.Domain.Models.Entities;

namespace ASP.Models
{
	public class PersonPageViewModel
	{
		public IEnumerable<Person> AllPersons { get; set; }
		public Person LastPersonCreated { get; set; }
	}
}