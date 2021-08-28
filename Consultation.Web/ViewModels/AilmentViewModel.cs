using Consultation.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Consultation.Web.ViewModels
{
    public class AilmentViewModel
    {
        // selectlist of patients (id, name)       
        //public SelectList Patients { set; get; }

        // Collecting PatientId and Issue in Form
        [Required]
        public int PatientId { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Issue { get; set; }

        public List<int> SelectedSymptomIds { get; set;}

        public MultiSelectList Symptoms { get; set;}
    }
}
