
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Exam.Models
{
    public class AppUser:IdentityUser
    {
        [MinLength(2),MaxLength(25)]
        public string Name  { get; set; }
        [MinLength(2), MaxLength(30)]
        public string Surname  { get; set; }
    }
}
