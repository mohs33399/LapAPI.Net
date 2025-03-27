using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.ViewModels.Theacher
{
    public class AddTeacherViewModel
    {


        [Required(ErrorMessage = "Please enter a name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be more than 3 and less than 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a subject")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Subject must be more than 3 and less than 50 characters")]

        public string Subject { get; set; }

        [Required(ErrorMessage = "Please enter an email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a phone number")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string Phone { get; set; }

        public List<string> ? Paths { get; set; } = new List<string>();
       
        public IFormFileCollection TeacherAttachments { get; set; }

    }
}


