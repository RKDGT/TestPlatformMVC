using System.ComponentModel.DataAnnotations;

namespace TestPlatform.BLL.DTO
{
    public class SignInModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        //[Display(Name = "Enter password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,16}$")]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}
