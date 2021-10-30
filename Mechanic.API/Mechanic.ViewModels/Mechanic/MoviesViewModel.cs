using System;
using System.Collections.Generic;
using System.Text;

namespace Mechanic.ViewModels.Mechanic
{
    public class MoviesViewModel
    {
		public MoviesViewModel(int accountId)
		{
			ID = accountId;
		}
		public long ID { get; set; }
		public long GENER_ID { get; set; }
		public string MOVIE_NAME { get; set; }
		public string MOVIE_DESCRIPTION { get; set; }
		public DateTime RELEASE_DATE { get; set; }
		public string GENRES_ASSOCIATED { get; set; }
		public string DURATION { get; set; }
		public string RATING { get; set; }
		public string CREATED_BY { get; set; }
		public DateTime CREATED_DATE { get; set; }
		public string MODIFIED_BY { get; set; }
		public DateTime MODIFIED_DATE { get; set; }
		public bool DELETED { get; set; }
	}
}
