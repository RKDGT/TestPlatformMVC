using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPlatform.BLL.Services;
using TestPlatform.BLL.DTO;
namespace TestPlatformMVC.Controllers
{
    public class SignInUpOutController : Controller
    {
        private readonly ISignInUpOutLogic _logic;
        public SignInUpOutController(ISignInUpOutLogic logic)
        {
            _logic = logic;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> Out()
        {
            await _logic.Out();
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> In(SignInModel signIn)
        {
            if (ModelState.IsValid)
            {
                bool res = await _logic.In(signIn);
                if (res)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login or Password");
            }
            return View(signIn);
        }

        public async Task<ActionResult> Up(SignUpModel signUp)
        {
            if (ModelState.IsValid)
            {
                var result = await _logic.Up(signUp);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(signUp);
        }
    }
}
