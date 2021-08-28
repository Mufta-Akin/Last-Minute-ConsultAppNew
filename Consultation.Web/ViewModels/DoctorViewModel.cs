using Consultation.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Consultation.Web.ViewModels
{
    public class DoctorViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Speciality Speciality { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(15)]
        public string Mobile { get; set; }
   
        public Doctor ToDoctor()
        {
            return new Doctor {
                Id = Id,
                Speciality = Speciality,
                Mobile = Mobile,
                User = new User 
                {
                    Name = Name,
                    Password = Password,
                    Email = Email
                }    
            };
        }

        public static DoctorViewModel FromDoctor(Doctor d)
        {
            return new DoctorViewModel
            {
                Id = d.Id,
                Speciality = d.Speciality,
                Mobile = d.Mobile,
                Name = d.User.Name,
                Password = d.User.Password,
                Email = d.User.Email
            };
        }
    }


}
