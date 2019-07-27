using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CothBazar.Controllers
{
    public class HomeController : Controller
    {
        //CatagoriesServices catagoriesServices = new CatagoriesServices();

       


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


        public ActionResult Adminitrative()
        {
            ViewBag.Message = "Your Adminitrative page.";

            return View();
        }

        public ActionResult Dispenrasy()
        {
            ViewBag.Message = "Your Dispensary page.";

            return View();
        }

        public ActionResult StoreHouse()
        {
            ViewBag.Message = "Your Dispensary page.";

            return View();
        }
        [HttpGet]
        public ActionResult log() {
            return View();

        }

        [HttpPost]
        public ActionResult log(string email,string Password)
        {

           return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.FeaturedCatagories = CatagoriesServices.Instance.GetFeaturedCatagories();

            return View(homeViewModel);
        }

        [HttpPost]
        public ActionResult Index(string uname,string psw)
        {

            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.ifTrue = 0;
            homeViewModel.FeaturedCatagories = CatagoriesServices.Instance.GetFeaturedCatagories();
            if (StudentServiceControll.Instance.IsRemain(uname,psw)) {
                homeViewModel.ifTrue = 1;
            }
                return View(homeViewModel);
        }

    }
}