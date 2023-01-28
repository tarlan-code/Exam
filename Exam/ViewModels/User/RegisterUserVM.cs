using System.ComponentModel.DataAnnotations;

namespace Exam.ViewModels.User
{
    public class RegisterUserVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password)]
        public string ConfirmPassword { get; set; }
    }
}
