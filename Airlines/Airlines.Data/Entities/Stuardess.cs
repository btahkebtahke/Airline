using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlines.Data.Entities
{
    public class Stuardess : Person
    {
        public int? TeamID { get; set; }
        [ForeignKey("TeamID")]
        public RaceTeam Team { get; set; }
        [Required(ErrorMessage = "Please enter the LastName")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Please use only alphabet characters")]
        [StringLength(50, ErrorMessage = "Do not use more than 50 charaacters")]
        public string Form { get; set; }
    }
}
