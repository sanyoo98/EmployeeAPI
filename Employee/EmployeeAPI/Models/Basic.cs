using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace EmployeeAPI.Models
{
    public class Basic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ? Id { get; set; }
        
       // [Required]
        public string Name { get; set; }

        public string Location { get; set; }
    }
}
