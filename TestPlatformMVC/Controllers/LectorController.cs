using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestPlatformMVC.Controllers
{
    public class LectorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
