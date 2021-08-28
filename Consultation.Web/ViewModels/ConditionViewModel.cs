using Consultation.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Consultation.Web.ViewModels
{
    public class ConditionViewModel
    {
        [Required]
        public string Name { get; set; }

        public Condition Condition { get; set; }
        [Required]
        public DateTime DiagnosedDate { get; set; }

        public IList<ConditionSymptom> ConditionSymptoms { get; set; }
    }
}
