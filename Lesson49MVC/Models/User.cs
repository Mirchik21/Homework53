using Microsoft.AspNetCore.Identity;
using System;

namespace Lesson49MVC.Models
{
    public class User: IdentityUser
    {
        public DateTime DateOfBirth { get; set; }
    }
}
