using System;

namespace ASP.Models
{
	public class EditPersonFormModel
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Line1 { get; set; }
		public string Line2 { get; set; }
		public string TownCity { get; set; }
		public string County { get; set; }
		public string PostCode { get; set; }
	}
}