using System.ComponentModel.DataAnnotations;

namespace Exam.ViewModels.User
{
    public class RegisterUserVM
    {
        [MinLength(2),MaxLength(25)]
        public string Name { get; set; }
        [MinLength(2), MaxLength(30)]

        public string Surname { get; set; }
        public string Username { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
