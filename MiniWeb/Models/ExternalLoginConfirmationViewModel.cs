using System.ComponentModel.DataAnnotations;

namespace MiniWeb.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
    }
}