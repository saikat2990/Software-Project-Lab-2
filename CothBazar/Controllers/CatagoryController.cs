using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Services;
using System.Data.Entity;

namespace CothBazar.Controllers
{
   // [Authorize(Roles ="Admin,Manager")]
    public class CatagoryController : Controller
    {
        // GET: Catagory
        //CatagoriesServices catagoriesServices = new CatagoriesServices();


        [HttpGet]
        public ActionResult Index()
        {
            //var catagories = catagoriesServices.GetCatagories();
            //List<Catagory> cat = catagoriesServices.GetCatagories();
            //return View(catagories);
            return View();
        }
        [HttpGet]
        public ActionResult CatagoryTable(String search,int ? pageNo)
        {
            CatagorySearchViewModel model = new CatagorySearchViewModel();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.SearchTerm = search;
            var totalRecords = CatagoriesServices.Instance.GetCatagoriesCount(search);
            var catagories = CatagoriesServices.Instance.GetCatagories(search,pageNo.Value);
            model.Catagories = catagories;

            if (model.Catagories != null)
            {
                model.Pager = new Pager(totalRecords, pageNo,3);
                //model.Catagories = model.Catagories.OrderBy(x => x.Id).Skip((page - 1)*pageSize).Take(pageSize).ToList();
                return PartialView(model);
            }
            else
            {
                return HttpNotFound();
            }

        }
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(Catagory catagory)
        {
            if (ModelState.IsValid)
            {
                CatagoriesServices.Instance.SaveCatagory(catagory);
                return RedirectToAction("CatagoryTable");
            }
            else {
                return new HttpStatusCodeResult(500);
            }

        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var catagory = CatagoriesServices.Instance.GetCatagory(Id);
            return PartialView(catagory);
        }
        [HttpPost]
        public ActionResult Edit(Catagory catagory)
        {
            CatagoriesServices.Instance.Update(catagory);
            return RedirectToAction("CatagoryTable");
        }
        //[HttpGet]
        //public ActionResult Delete(int Id)
        //{
        //    var catagory = CatagoriesServices.Instance.GetCatagory(Id);
        //    return View(catagory);
        //}
        [HttpPost]
        public ActionResult Delete(int Id)
        {
           // catagory = CatagoriesServices.Instance.GetCatagory(catagory.Id);
            CatagoriesServices.Instance.Delete(Id);
            return RedirectToAction("CatagoryTable");
        }
    }
}