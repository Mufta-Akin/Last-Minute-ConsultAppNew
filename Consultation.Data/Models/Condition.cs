using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Consultation.Data.Models
{
    public class Condition
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime DiagnosedDate { get; set; } = DateTime.Now;

        public string Description { get; set; }

        public IList<ConditionSymptom> ConditionSymptoms { get; set; }
    }
}
