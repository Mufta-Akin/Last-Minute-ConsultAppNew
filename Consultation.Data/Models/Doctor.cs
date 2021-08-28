using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Consultation.Data.Models
{
    public enum Speciality { Cardiology, Neurology, Urology, Dermatology, Radiology, Family_Medicine, Anesthesiology, Hematology, Obstetrics_Gynecology, Pediatrics, Psychiatry, Respirology, Rheumatology }

    public class Doctor
    {
        public int Id { get; set; }

        [Required]
        public Speciality Speciality { get; set; }

        [Required]
        [StringLength(15)]
        public string Mobile { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
