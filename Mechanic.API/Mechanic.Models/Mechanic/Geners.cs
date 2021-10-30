using System;
using System.Collections.Generic;
using System.Text;

namespace Mechanic.Models.Mechanic
{
    public class Genres
    {
		public int ID { get; set; }
		public string NAME { get; set; }
		public string DESCRIPTION { get; set; }
		public string CREATED_BY { get; set; }
		public DateTime CREATED_DATE { get; set; }
		public string MODIFIED_BY { get; set; }
		public DateTime MODIFIED_DATE { get; set; }
		public bool DELETED { get; set; }
	}
}
