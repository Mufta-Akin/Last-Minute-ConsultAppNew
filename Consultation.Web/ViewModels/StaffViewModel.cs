using Consultation.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Consultation.Web.ViewModels
{
    public class StaffViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(15)]
        public string Mobile { get; set; }


        public Staff ToStaff()
        {
            return new Staff
            {
                Id = Id,
                Position = Position,
                Mobile = Mobile,
                User = new User
                {
                    Name = Name,
                    Password = Password,
                    Email = Email
                }
            };
        }

        public static StaffViewModel FromStaff(Staff s)
        {
            return new StaffViewModel
            {
                Id = s.Id,
                Position = s.Position,
                Mobile = s.Mobile,
                Name = s.User.Name,
                Password = s.User.Password,
                Email = s.User.Email
            };
        }
    }

}
