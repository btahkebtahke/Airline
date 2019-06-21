using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlines.Data.Entities
{
    public class Person : BaseEntity
    {
        [Required(ErrorMessage = "Please enter the FirstName")]
        [RegularExpression(@"^[a-zA-Z ]*$",ErrorMessage = "Please use only alphabet characters")]
        [StringLength(50,ErrorMessage = "Do not use more than 50 charaacters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter the LastName")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Please use only alphabet characters")]
        [StringLength(50, ErrorMessage = "Do not use more than 50 charaacters")]
        public string LastName { get; set; }
    }
}
