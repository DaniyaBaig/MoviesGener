using System;
using System.Collections.Generic;
using System.Text;

namespace Mechanic.ViewModels.Mechanic
{
    public class GenresViewModel
    {

        public GenresViewModel(int accountId)
        {
            ID = accountId;
        }
        public long ID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
        public bool DELETED { get; set; }
    }
}
