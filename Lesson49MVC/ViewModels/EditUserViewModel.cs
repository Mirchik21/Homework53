using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace Lesson49MVC.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }
    }
}
