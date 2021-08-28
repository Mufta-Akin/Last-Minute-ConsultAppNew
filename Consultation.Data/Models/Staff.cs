using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Consultation.Data.Models
{

    public class Staff
    {
        public int Id { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        [StringLength(15)]
        public string Mobile { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
