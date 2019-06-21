using Airlines.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Airlines.WebUI.Models
{
    public class AdminViewModel
    {
        public virtual IEnumerable<Stuardess> Stuardesses { get; set; }
        public virtual IEnumerable<Race> Races { get; set; }
        public virtual IEnumerable<Navigator> Navigators { get; set; }
        public virtual IEnumerable<RadioMan> RadioMen { get; set; }
        public virtual IEnumerable<Query> Queries { get; set; }
        public virtual IEnumerable<Pilot> Pilots { get; set; }
        public virtual RaceTeam RaceTeam{ get; set; }

    }
}