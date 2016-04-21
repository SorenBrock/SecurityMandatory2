using System.ComponentModel.DataAnnotations;

namespace securityMandatory2.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = @"Indtast venligst brugernavn")]
        [RegularExpression("^[A-Za-z0-9_]+$", ErrorMessage = @"Du må kun bruge a-z / 0-9 i brugernavn")]
        [StringLength(64, ErrorMessage = @"Brugernavnet skal minimum være {2} karakter langt", MinimumLength = 4)]
        [Display(Name = @"Brugernavn")]
        public string Username { get; set; }

        [Required(ErrorMessage = @"Indtast venligst password")]
        [DataType(DataType.Password)]
        [RegularExpression("^[A-Za-z0-9_]+$", ErrorMessage = @"Du må kun bruge a-z / 0-9 i password")]
        [StringLength(64, ErrorMessage = @"Passwordet skal minimum være {2} karakter langt", MinimumLength = 8)]
        [Display(Name = @"Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = @"Password (confirmation)")]
        public string ConfirmPassword { get; set; }

    }
}