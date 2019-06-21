using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlines.Data.Entities
{
    public class Query : BaseEntity
    {
        [ForeignKey("RaceTeamID")]
        public RaceTeam RaceTeam { get; set; }
        public int RaceTeamID { get; set; }
        [ForeignKey("RaceID")]
        public Race Race { get; set; }
        public int RaceID { get; set; }
        public bool? IsAccepted { get; set; }
    }
}
