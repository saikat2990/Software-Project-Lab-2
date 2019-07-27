using ClothBazar.Entities;
using ClothBazar.Sevices;
using ClothBazar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CothBazar.Controllers
{
    public class DrugController : Controller
    {
        // GET: Drug
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DrugTable(String search, int? pageNo)
        {
            DrugServicesVeiwModel model = new DrugServicesVeiwModel();

            //model.SearchItem = search;

            model.PageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = DrugServices.Instance.GetDrugsCount(search);

            model.Drugs = DrugServices.Instance.GetDrugs(model.PageNo);

            // model.Products = ProductsServices.Instance.GetProducts(search, pageNo.Value, 6);

            //model.Pager = new Pager(totalRecords, pageNo, 6);

            if (!string.IsNullOrEmpty(search))
            {
                model.SearchItem = search;
                model.Drugs = model.Drugs.Where(p => p.Name != null && p.Name.Contains(search)).ToList();
            }

            return PartialView(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            //CatagoriesServices catagoriesServices = new CatagoriesServices();

            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(Drug drug)
        {
            // CatagoriesServices catagoriesServices = new CatagoriesServices();


            var product = new Drug();

            product.Name = drug.Name;
            product.DespensaryAmount = drug.DespensaryAmount;
            product.StoreHouseAmount = drug.StoreHouseAmount;
            product.StudentSeriveAmount = drug.StudentSeriveAmount;
            product.StafftSeriveAmount = drug.StafftSeriveAmount;
            product.PricePerTab = drug.PricePerTab;
            product.Description = drug.Description;
            product.ProductionDate = drug.ProductionDate;
            product.ExpireDate = drug.ExpireDate;

            DrugServices.Instance.SaveDrug(product);
            return RedirectToAction("DrugTable");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            Drug product = new Drug();

            var drug = DrugServices.Instance.GetDrug(Id);

            product.Id = drug.Id;
            product.Name = drug.Name;
            product.DespensaryAmount = drug.DespensaryAmount;
            product.StoreHouseAmount = drug.StoreHouseAmount;
            product.StudentSeriveAmount = drug.StudentSeriveAmount;
            product.StafftSeriveAmount = drug.StafftSeriveAmount;
            product.PricePerTab = drug.PricePerTab;
            product.Description = drug.Description;
            product.ProductionDate = drug.ProductionDate;
            product.ExpireDate = drug.ExpireDate;


            //product.catagory != null ? product.catagory.Id : 0;

            return PartialView(product);
        }
        [HttpPost]
        public ActionResult Edit(Drug drug)
        {
            var product = DrugServices.Instance.GetDrug(drug.Id);

            product.Name = drug.Name;
            product.DespensaryAmount = drug.DespensaryAmount;
            product.StoreHouseAmount = drug.StoreHouseAmount;
            product.StudentSeriveAmount = drug.StudentSeriveAmount;
            product.StafftSeriveAmount = drug.StafftSeriveAmount;
            product.PricePerTab = drug.PricePerTab;
            product.Description = drug.Description;
            product.ProductionDate = drug.ProductionDate;
            product.ExpireDate = drug.ExpireDate;

            DrugServices.Instance.UpdateDrug(product);
            return RedirectToAction("DrugTable");

        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            var product = DrugServices.Instance.GetDrug(Id);
            DrugServices.Instance.DeleteDrug(product);
            return RedirectToAction("DrugTable");
        }
        [HttpGet]
        public ActionResult StoreToDispesaries() {

            Requestclass Requestdrug = new Requestclass();
            Requestdrug = DrugServices.Instance.GEtAllDrug();

            return PartialView(Requestdrug);
        }
        [HttpPost]
        public ActionResult StoreToDispesaries(Requestclass drugClass)
        {

            Requestclass drugClass1 = new Requestclass();
            //drugClass.drugs = DrugServices.Instance.GEtAllDrug();
            TempData["mydata"] = drugClass;
            

            return RedirectToAction("ApprovalByStore");
        }
        [HttpGet]
        public ActionResult ApprovalByStore()
        {
            Requestclass drugClass = TempData["mydata"] as Requestclass;
            
            return PartialView(drugClass);
        }
        [HttpPost]
        public ActionResult ApprovalByStore(Requestclass drugClass)
        {


            TempData["mydata"] = drugClass;
            DrugServices.Instance.SaveDrugClass(drugClass);

            foreach (DrugRequest v in drugClass.Drugs) {

                Drug maindrug = DrugServices.Instance.GetDrug(v.DrugName);
                maindrug.DespensaryAmount += v.Amount;
                maindrug.StoreHouseAmount -= v.Amount;
                maindrug.StudentSeriveAmount += v.Amount;
                DrugServices.Instance.UpdateDrug(maindrug);


                DispensaryDrugTable d = DispensaryDrugServices.Instance.GetDispensaryDrugServices(v.DrugName);
                d.RemainAmount += v.Amount;
                DispensaryDrugServices.Instance.Update(d);
            }


            return RedirectToAction("Index");
        }
        //[HttpGet]
        //public ActionResult ApprovalByStoreAndDatabase()
        //{
        //    Requestclass drugClass = TempData["mydata"] as Requestclass;


        //  //  DrugServices.Instance.SaveDrugClass(drugClass);

        //    return PartialView(drugClass);
        //}

    }
}