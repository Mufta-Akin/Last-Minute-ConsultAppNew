using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Consultation.Data.Models
{
    public enum SymptomSignificance { Primary, Secondary, Tertiary }
    public class ConditionSymptom
    {
        public int Id { get; set; }
        public int ConditionId { get; set; }
        public Condition Condition { get; set; }
        public int SymptomId { get; set; }
        public Symptom Symptom { get; set; }
        public SymptomSignificance Significance { get; set; } = SymptomSignificance.Primary;
    }
}
