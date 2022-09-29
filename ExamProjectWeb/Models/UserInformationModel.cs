using System.ComponentModel.DataAnnotations;

namespace ExamProjectWeb.Models
{
    //The Database's model structure
    public class UserInformationModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool IsAdmin { get; set; } = false;
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}
