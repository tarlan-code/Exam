using Exam.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Exam.Models
{
    public class Position: BaseEntity
    {
        [MinLength(2),MaxLength(30)]
        public string Name { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
