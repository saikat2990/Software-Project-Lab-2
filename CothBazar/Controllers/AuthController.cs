using ClothBazar.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CothBazar.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Name, string Email, string Password)
        {
            if (StudentServiceControll.Instance.IsRemain(Name, Password))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Regis");
            }
                
        }

        [HttpGet]
        public ActionResult Regis()
        {
            return View();
        }

    }
}