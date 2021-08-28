using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Consultation.Data.Models
{
    public class Diagnosis
    {
        public int Id { get; set; }

        public Condition Condition { get; set; }
        public DateTime DiagnosedOn { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ConfirmedOn { get; set; }
        public int DoctorId { get; set; }//may have to convert to nullable int?
        public Doctor Doctor { get; set; }

        public int PatientId { get; set; }//may have to convert to nullable int?
        public Doctor Patient { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 5)]
        public string Description { get; set; }
        public IList<Symptom> Symptoms { get; internal set; }
    }
}
