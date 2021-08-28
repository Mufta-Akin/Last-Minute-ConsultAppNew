using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Consultation.Data.Models
{
    public class Ailment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Issue { get; set; }

        [StringLength(500)]
        public string Resolution { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime ResolvedOn { get; set; } = DateTime.MinValue;

        public bool Active { get; set; } = true;

        // Foreign key relating to Patient with the ailment
        public int PatientId { get; set; }
        public IList<AilmentSymptom> Symptoms { get; set; }
        public Patient Patient { get; set; }


    }
}
