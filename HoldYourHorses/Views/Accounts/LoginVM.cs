using System.ComponentModel.DataAnnotations;

namespace HoldYourHorses.Views.Accounts
{
    public class LoginVM
    {
        [Display(Name = "Email-adress")]
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
