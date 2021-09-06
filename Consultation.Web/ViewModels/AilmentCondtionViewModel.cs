using Consultation.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Consultation.Web.ViewModels
{
    public class AilmentConditionViewModel
    {
        public int Id { get; set; }
        public string Issue { get; set; }
        public string Resolution { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime ResolvedOn { get; set; } = DateTime.MinValue;
        public bool Active { get; set; } = true;

        // Foreign key relating to Patient with the ailment
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientMobile { get; set; }
        public IList<AilmentSymptom> Symptoms { get; set; }

        public IList<Condition> PossibleConditions { get; set; } = new List<Condition>();

    }
}
