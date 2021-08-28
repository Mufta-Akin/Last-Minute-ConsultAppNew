using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Consultation.Data.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [StringLength(15)]
        public string Mobile { get; set; }

        [Required]
        public DateTime Dob { get; set; }
        
        public int Age => new DateTime((DateTime.Now - Dob).Ticks).Year;//datetime dob with readonly property to calculate age

        public IList<Ailment> Ailments { get; set; } = new List<Ailment>();

        // Related User Foreign Key
        public int UserId { get; set; }

        // Related User Account
        public User User { get; set; }
    }
}
