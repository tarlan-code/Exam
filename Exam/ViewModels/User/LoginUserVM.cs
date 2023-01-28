using System.ComponentModel.DataAnnotations;

namespace Exam.ViewModels.User
{
    public class LoginUserVM
    {
        public string UsernameOrEmail { get; set; }
        [DataType(DataType.Password)]
        public string  Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
