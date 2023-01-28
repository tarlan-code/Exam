using Exam.Models;
using System.ComponentModel.DataAnnotations;

namespace Exam.ViewModels.Employee
{
    public class CreateEmployeeVM
    {
        [MinLength(3), MaxLength(25)]
        public string Name { get; set; }
        [MinLength(3), MaxLength(30)]
        public string Surname { get; set; }
        public IFormFile Image { get; set; }
        [Url]
        public string? Linkedin { get; set; }
        [Url]

        public string? Facebook { get; set; }
        [Url]

        public string? Twitter { get; set; }

        public int PositionId { get; set; }
      
    }
}
