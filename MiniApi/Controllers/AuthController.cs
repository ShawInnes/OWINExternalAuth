using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using MiniApi.Models;
using Serilog;
using Serilog.Context;

namespace MiniApi.Controllers
{
    public class AuthController : ApiController
    {
        // POST api/auth
        public LoginResponse Post([FromBody]LoginModel value)
        {
            using (LogContext.PushProperty("CorrelationId", value.CorrelationId))
            {
                Log.Information("AuthController.Post {@LoginModel}", value);

                if (value.UserName == "shaw" && value.Password == "test")
                {
                    Log.Information("Authentication Success");

                    try
                    {
                        var response = new LoginResponse()
                        {
                            Id = Guid.NewGuid().ToString("N"),
                            UserName = value.UserName,
                            Balance = 1000.0
                        };

                        return response;
                    }
                    catch (Exception ex)
                    {

                        Log.Error(ex, "Response Failed");
                    }
                }

                Log.Information("Authentication Failed");

                return null;
            }
        }
    }
}
