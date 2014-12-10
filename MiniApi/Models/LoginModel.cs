using System;
using Serilog.Extras.Attributed;

namespace MiniApi.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        
        [NotLogged]
        public string Password { get; set; }
        
        public Guid CorrelationId { get; set; }
    }
}