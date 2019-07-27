using ClothBazar.Database;
using ClothBazar.Entities;
using ClothBazar.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace CothBazar.Controllers
{
    public class DispensaryController : Controller
    {
        // GET: Dispensary
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Appoinment(string search,int? pageNo) {

            DispensaryVeiwModel model = new DispensaryVeiwModel();

            //model.SearchItem = search;

            model.PageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

           // var totalRecords = DispensariesServices.Instance.GetDispensaryCount(search);

            model.DispensaryServices = DispensariesServices.Instance.GetDispensaryDrugs(model.PageNo);

            //model.Products = ProductsServices.Instance.GetProducts(search, pageNo.Value, 6);

            //model.Pager = new Pager(totalRecords, pageNo, 6);

            if (!string.IsNullOrEmpty(search))
            {
                model.SearchItem = search;
                model.DispensaryServices = model.DispensaryServices.Where(p => p.Name != null && p.Name.Contains(search)).ToList();
            }

            return PartialView(model);
        }

        [HttpGet]
        public ActionResult Create() {

           // DispensaryDrugTable dispensaryDrugTable = new DispensaryDrugTable();
            return PartialView();
        }
        string name, email;
        string serials;
        [HttpPost]
        public ActionResult Create(DispensaryServices dispensaryServices)
        {

            DispensariesServices.Instance.SaveService(dispensaryServices);

            TempData["name"] = dispensaryServices.Name;
            TempData["email"] = dispensaryServices.Email;
            TempData["serial"] = dispensaryServices.SerialNum.ToString();

            return RedirectToAction("UpdateDispensaryDrugList");
            //UpdateDispensaryDrugList
        }

        [HttpGet]
        public ActionResult UpdateDispensaryDrugList()
        {
            DispensaryDrugTableViewModel model = new DispensaryDrugTableViewModel();
            model.DispensaryDrugs = DispensaryDrugServices.Instance.GetDispensaryAllDrug();
            return PartialView(model);

        }
        [HttpPost]
        public ActionResult UpdateDispensaryDrugList(DispensaryDrugTableViewModel model)
        {
            TempData["mydata"] = model;

            string totalDrug="";
            decimal cost = 0;


            for (int i = 0; i < model.DispensaryDrugs.Count(); i++) {
                totalDrug += model.DispensaryDrugs[i].Name+"("+Convert.ToString(model.DispensaryDrugs[i].ServiceAmount)+")"+" ";
                cost += (model.DispensaryDrugs[i].PricePerTab * model.DispensaryDrugs[i].ServiceAmount);
            }


            for (int i = 0; i < model.DispensaryDrugs.Count(); i++)
            {
                DispensaryDrugTable d = DispensaryDrugServices.Instance.GetDispensaryDrugServices(model.DispensaryDrugs[i].Name);
                d.RemainAmount -= model.DispensaryDrugs[i].ServiceAmount;

                if (d.RemainAmount < 100) {

                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Credentials = new NetworkCredential("bsse0910@iit.du.ac.bd", "masksenma");
                    MailMessage msg = new MailMessage();
                    msg.To.Add("bsse0905@iit.du.ac.bd");
                    msg.From = new MailAddress("bsse0905@iit.du.ac.bd");
                    msg.Subject = "From DUMC for appionment";
                    msg.Body = d.Name+" is out of scope , now its remaining "+d.RemainAmount;

                    client.Send(msg);
                }
                DispensaryDrugServices.Instance.Update(d);
            }

            name = TempData["name"] as string;
            email = TempData["email"] as string;
            serials = TempData["serial"] as string;
            int serial = Convert.ToInt32(serials);
            List<DispensaryServices> dispensaryServices = DispensariesServices.Instance.GetDispensaryService(name,email,serial);

            DispensaryServices newM = dispensaryServices[0];
            DispensaryServices edit = DispensariesServices.Instance.GetDispensaryService(newM.Id);
            edit.DrugService = totalDrug;
            edit.TotalCost = cost;
            DispensariesServices.Instance.Update(edit);

            SmtpClient client1 = new SmtpClient("smtp.gmail.com", 587);
            client1.EnableSsl = true;
            client1.DeliveryMethod = SmtpDeliveryMethod.Network;
            client1.Credentials = new NetworkCredential("bsse0910@iit.du.ac.bd", "masksenma");
            MailMessage msg1 = new MailMessage();
            msg1.To.Add("bsse0905@iit.du.ac.bd");
            msg1.From = new MailAddress("bsse0905@iit.du.ac.bd");
            msg1.Subject = "From DUMC for appionment";
            msg1.Body = " Name: "+edit.Name+" Problem: "+edit.Problem+" Serial Num: "+edit.SerialNum
                +" DoctorName: "+edit.DoctorName+" DrugServices: "+edit.DrugService;

            client1.Send(msg1);



            return PartialView("Index");

        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            DispensaryServices dispensaryServices = new DispensaryServices();
            dispensaryServices = DispensariesServices.Instance.GetDispensaryService(Id);
            return PartialView(dispensaryServices);

        }

        [HttpPost]
        public ActionResult Edit(DispensaryServices dispensaryServices) {

            DispensaryServices edit = DispensariesServices.Instance.GetDispensaryService(dispensaryServices.Id);

            edit.Date= dispensaryServices.Date;
            edit.DoctorName = dispensaryServices.DoctorName;
            edit.DrugService = dispensaryServices.DrugService;
            edit.Email = dispensaryServices.Email;
            edit.Name = dispensaryServices.Name;
            edit.Problem = dispensaryServices.Problem;
            edit.SerialNum = dispensaryServices.SerialNum;
            edit.TotalCost = dispensaryServices.TotalCost;

            DispensariesServices.Instance.Update(edit);
            return RedirectToAction("Appoinment");
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            var product = DispensariesServices.Instance.GetDispensaryService(Id);
            DispensariesServices.Instance.DeleteProduct(product);
            return RedirectToAction("Appoinment");
        }
    }
}