using System;
using System.Collections.Generic;
using System.Text;

namespace Mechanic.Models.Mechanic
{
    public class CreateGenres : Genres
    {
        public CreateGenres(int id = 1, string name = null, string description = null)
        {
            ID = id ;
            NAME = name;
            DESCRIPTION = description;            
        }
    }
}
