using ClothBazar.Entities;
using ClothBazar.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CothBazar.Controllers
{
    public class DispensariesDrugController : Controller
    {
        // GET: DispensariesDrug
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DispensaryDrugTable(string search, int? pageNo) {

            DispensaryDrugTableViewModel model = new DispensaryDrugTableViewModel();

            model.PageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            model.DispensaryDrugs = DispensaryDrugServices.Instance.GetDrugs(model.PageNo);

            //model.Products = ProductsServices.Instance.GetProducts(search, pageNo.Value, 6);

            //model.Pager = new Pager(totalRecords, pageNo, 6);

            if (!string.IsNullOrEmpty(search))
            {
                model.SearchItem = search;
                model.DispensaryDrugs = model.DispensaryDrugs.Where(p => p.Name != null && p.Name.Contains(search)).ToList();
            }

            return PartialView(model);
        }


        [HttpGet]
        public ActionResult Create()
        {

            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(DispensaryDrugTable dispensaryDrugTable)
        {

            DispensaryDrugServices.Instance.SaveService(dispensaryDrugTable);
            return RedirectToAction("DispensaryDrugTable");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            DispensaryDrugTable dispensaryDrugTable = new DispensaryDrugTable();
            dispensaryDrugTable = DispensaryDrugServices.Instance.GetDispensaryDrugServices(Id);
            return PartialView(dispensaryDrugTable);

        }

        [HttpPost]
        public ActionResult Edit(DispensaryDrugTable dispensaryDrugTable)
        {

            DispensaryDrugTable edit = DispensaryDrugServices.Instance.GetDispensaryDrugServices(dispensaryDrugTable.Id); 

            edit.Name = dispensaryDrugTable.Name;
            edit.TotalAmount = dispensaryDrugTable.TotalAmount;
            edit.ServiceAmount = dispensaryDrugTable.ServiceAmount;
            edit.RemainAmount = dispensaryDrugTable.RemainAmount;
            edit.StafftSeriveAmount = dispensaryDrugTable.StafftSeriveAmount;
            edit.PricePerTab = dispensaryDrugTable.PricePerTab;
            edit.Description = dispensaryDrugTable.Description;
            edit.ProductionDate = dispensaryDrugTable.ProductionDate;
            edit.ExpireDate = dispensaryDrugTable.ExpireDate;

            DispensaryDrugServices.Instance.Update(edit);
            return RedirectToAction("DispensaryDrugTable");
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            var product = DispensaryDrugServices.Instance.GetDispensaryDrug(Id);
            DispensaryDrugServices.Instance.DeleteProduct(product);
            return RedirectToAction("DispensaryDrugTable");
        }


    }
}