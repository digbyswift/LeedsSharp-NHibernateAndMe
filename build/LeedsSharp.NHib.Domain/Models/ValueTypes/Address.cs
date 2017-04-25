namespace LeedsSharp.NHib.Domain.Models.ValueTypes
{
	public class Address
	{
		public virtual string Line1 { get; set; }
		public virtual string Line2 { get; set; }
		public virtual string TownCity { get; set; }
		public virtual string County { get; set; }
		public virtual string PostCode { get; set; }
	}
}