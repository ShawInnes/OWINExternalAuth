using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MiniWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Cast the Thread.CurrentPrincipal
            var icp = Thread.CurrentPrincipal as System.Security.Claims.ClaimsPrincipal;

            // Access IClaimsIdentity which contains claims
            if (icp != null)
            {
                var claimsIdentity = (ClaimsIdentity)icp.Identity;

                return View(claimsIdentity.Claims);
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}