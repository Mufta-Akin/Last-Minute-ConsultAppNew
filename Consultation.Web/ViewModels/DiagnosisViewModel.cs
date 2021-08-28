using Consultation.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace Consultation.Web.ViewModels
{
    public class DiagnosisViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Condition Condition { get; set; }

        [Required]
        public DateTime DiagnosedOn { get; set; }

        [Required]
        public DateTime ConfirmedOn { get; set; }

        public int PatientId { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }
    }

}
