using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExamProjectWeb.Models
{
    //The registration form's model, which is needid to process it.
    public class UserRegistrationModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [DisplayName("Confirm Password")]
        public string PasswordConfirm { get; set; }
    }
}
