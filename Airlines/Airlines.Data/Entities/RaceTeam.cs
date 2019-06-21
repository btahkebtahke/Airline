using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlines.Data.Entities
{
    public class RaceTeam : BaseEntity
    {
        [ForeignKey("PilotID")]
        public Pilot Pilot { get; set; }
        public int? PilotID { get; set; }
        [ForeignKey("NavigatorID")]
        public Navigator Navigator { get; set; }
        public int? NavigatorID { get; set; }
        [ForeignKey("RadioManID")]
        public RadioMan RadioMan { get; set; }
        public int? RadioManID { get; set; }
        public ICollection<Stuardess> Stuardesses { get; set; }
        public bool? IsAccepted { get; set; }
    }
}
