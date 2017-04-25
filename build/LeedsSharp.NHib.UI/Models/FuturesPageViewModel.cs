using System;
using System.Collections.Generic;

namespace ASP.Models
{
	public class FuturesPageViewModel
	{
		public int PersonCount { get; set; }
		public DateTime LastEdit { get; set; }
		public IEnumerable<string> LastAssignedServer { get; set; }
	}
}