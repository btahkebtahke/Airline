using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlines.Data.Entities
{
    public class Race : BaseEntity
    {
        private string name;
        public string Name
        {
            get {
                name = Departure + "-" + Destinaton;
                return name;
                }
            set
            {
                name = value;
            }
        }
        [Required(ErrorMessage = "Please enter the Departure field")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Please use only alphabet characters")]
        [StringLength(50, ErrorMessage = "Do not use more than 50 charaacters")]
        public string Departure { get; set; }
        [Required(ErrorMessage = "Please enter the Destination field")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Please use only alphabet characters")]
        [StringLength(50, ErrorMessage = "Do not use more than 50 charaacters")]
        public string Destinaton { get; set; }
        [Required(ErrorMessage = "Please choose the date from today")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }
        public bool? IsDeparted { get; set; }
        [ForeignKey("RaceTeamID")]
        public RaceTeam RaceTeam { get; set; }

        public int? RaceTeamID { get; set; }
    }
}
