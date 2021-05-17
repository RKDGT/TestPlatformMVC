using System.ComponentModel.DataAnnotations;

namespace TestPlatform.BLL.DTO
{
    public class SignUpAdminModel
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string SurName { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,16}$", ErrorMessage = "Password must have from 8 to 16 character and contain lower, apper case letter number and at less one spetial symbol (?=.*!)")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,16}$")]
        public string RepeatPassword { get; set; }
    }
}
