using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models
{
    public class SignInInput
    {
        [Required]
        [Display(Name ="Email adresiniz:")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Şifreniz:")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Beni Hatırla:")]
        public bool IsRemember { get; set; }
    }
}
