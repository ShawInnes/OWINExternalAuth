using System;
using System.ComponentModel.DataAnnotations;

namespace MiniWeb.Models
{
    public class LoginViewModel
    {
        public Guid CorrelationId { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}