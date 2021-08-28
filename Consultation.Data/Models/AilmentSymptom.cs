using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Consultation.Data.Models
{
    public class AilmentSymptom
    {
        public int Id { get; set; }

        public int AilmentId { get; set; }
        public Ailment Ailment { get; set; }
        public int SymptomId { get; set; }
        public Symptom Symptom { get; set; }
        public SymptomSignificance Significance { get; set; } = SymptomSignificance.Primary; //enum
    }
}
