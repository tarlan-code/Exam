using Exam.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Exam.Models
{
    public class Employee: BaseEntity
    {
        [MinLength(3),MaxLength(25)]
        public string Name { get; set; }
        [MinLength(3), MaxLength(30)]
        public string Surname { get; set; }
        public string ImgUrl { get; set; }
        [Url]
        public string? Linkedin { get; set; }
        [Url]

        public string? Facebook { get; set; }
        [Url]

        public string? Twitter { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
