using System.ComponentModel.DataAnnotations;

namespace MiniWeb.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
    }
}
