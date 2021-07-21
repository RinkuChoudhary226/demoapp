using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld.Controllers
{
    [Route("ErrorPage/{statuscode}")]
    public class ErrorPageController : Controller
    {
        public IActionResult Index(int statuscode)
        {
            switch(statuscode)
            {
                case 404:
                    ViewData["Error"] = "Page Not Found";
                    break;
                default:
                    break;
            }
            return View("ErrorPage");
        }
    }
}
