using System;
using System.ComponentModel.DataAnnotations;
using Consultation.Data.Models;

namespace Consultation.Web.ViewModels
{
    public class PatientViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [StringLength(15)]
        public string Mobile { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime Dob { get; set; }

        public Patient ToPatient()
        {
            return new Patient {
                Id = Id,
                Address = Address,
                Mobile = Mobile,
                Dob = Dob,
                User = new User 
                {
                    Name = Name,
                    Password = Password,
                    Email = Email
                }    
            };
        }

        public static PatientViewModel FromPatient(Patient p)
        {
            return new PatientViewModel
            {
                Id = p.Id,
                Address = p.Address,
                Mobile = p.Mobile,
                Dob = p.Dob,
                Name = p.User.Name,
                Password = p.User.Password,
                Email = p.User.Email
            };
        }
    }

}
