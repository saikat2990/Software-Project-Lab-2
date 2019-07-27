using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Web.UI;

namespace CothBazar.Controllers
{
    public class WeeklyDoctorListController : Controller
    {
        // GET: WeeklyDoctorList
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult WeeklyDoctorList(string search, int? pageNo)
        {

            WeeklyDoctorListViewModel model = new WeeklyDoctorListViewModel();

            //model.SearchItem = search;

            model.PageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            // var totalRecords = DispensariesServices.Instance.GetDispensaryCount(search);

            model.WeeklyDoctorLists = WeeklyDoctorListServices.Instance.GetWeeklyDoctorLists(model.PageNo);

            //model.Products = ProductsServices.Instance.GetProducts(search, pageNo.Value, 6);

            //model.Pager = new Pager(totalRecords, pageNo, 6);



            if (!string.IsNullOrEmpty(search))
            {
                model.SearchItem = search;
                model.WeeklyDoctorLists = WeeklyDoctorListServices.Instance.GetWeeklyDoctorListRound(search);
            }

            return PartialView(model);
        }
        [HttpGet]
        public ActionResult Create()
        {

            WeeklyDoctorList weeklyDoctorList = new WeeklyDoctorList();
            return PartialView(weeklyDoctorList);
        }
        [HttpPost]
        public ActionResult Create(WeeklyDoctorList weeklyDoctorList)
        {

            WeeklyDoctorListServices.Instance.SaveService(weeklyDoctorList);
            return RedirectToAction("WeeklyDoctorList");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            WeeklyDoctorList weeklyDoctorList = new WeeklyDoctorList();
            weeklyDoctorList = WeeklyDoctorListServices.Instance.GetWeeklyDoctorList(Id);
            return PartialView(weeklyDoctorList);

        }
        [HttpPost]
        public ActionResult Edit(WeeklyDoctorList weeklyDoctorList)
        {

            WeeklyDoctorList edit = WeeklyDoctorListServices.Instance.GetWeeklyDoctorList(weeklyDoctorList.Id);

            edit.Date = weeklyDoctorList.Date;
            edit.DoctorName = weeklyDoctorList.DoctorName;
            edit.Day = weeklyDoctorList.Day;
            edit.Serial = weeklyDoctorList.Serial;
            edit.Time = weeklyDoctorList.Time;

            WeeklyDoctorListServices.Instance.Update(edit);
            return RedirectToAction("WeeklyDoctorList");
        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            var product = WeeklyDoctorListServices.Instance.GetWeeklyDoctorList(Id);
            WeeklyDoctorListServices.Instance.DeleteProduct(product);
            return RedirectToAction("WeeklyDoctorList");
        }
        [HttpGet]
        public ActionResult Appoint(int Id)
        {
            var product = ProductsServices.Instance.GetProduct(Id);
            WeeklyDoctorListViewModel model = new WeeklyDoctorListViewModel();
            var count = WeeklyDoctorListServices.Instance.GetCount();
            model.WeeklyDoctorLists = WeeklyDoctorListServices.Instance.GetAppointedDortorList(product.name);
            return View(model);
        }
        [HttpPost]
        public ActionResult Appoint(AppointmentList appoinmentList)
        {
            AppointmentList appointmentList1 = new AppointmentList();
            appointmentList1.Date = appoinmentList.Date;
            appointmentList1.DoctorName = appoinmentList.DoctorName;
            appointmentList1.Email = appoinmentList.Email;
            appointmentList1.Name = appoinmentList.Name;
            appointmentList1.Problem = appoinmentList.Problem;
            appointmentList1.SerialNum = appoinmentList.SerialNum;
            appointmentList1.Time = appoinmentList.Time;
            if (WeeklyDoctorListServices.Instance.IsRemain(appoinmentList))
            {
                WeeklyDoctorList temp = WeeklyDoctorListServices.Instance.GetSerialNum(appoinmentList);
                WeeklyDoctorList upload = WeeklyDoctorListServices.Instance.GetWeeklyDoctorList(temp.Id);
                upload.Serial++;
                WeeklyDoctorListServices.Instance.Update(upload);
                appointmentList1.SerialNum = upload.Serial;
                AppoinmentListsServices.Instance.SaveService(appointmentList1);

                SmtpClient client = new SmtpClient("smtp.gmail.com",587);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential("bsse0910@iit.du.ac.bd","masksenma");
                MailMessage msg = new MailMessage();
                msg.To.Add(appointmentList1.Email);
                msg.From = new MailAddress(appointmentList1.Email);
                msg.Subject = "From DUMC for appionment";
                msg.Body = "Name: "+appointmentList1.Name.ToString() +"Problem: "+appointmentList1.Problem.ToString() +"Doctor Name: "+
                            appointmentList1.DoctorName.ToString() +"Date: "+appointmentList1.Date.ToString() +
                            "Time: "+appointmentList1.Time.ToString() +"Serial No:" +appointmentList1.SerialNum.ToString();

                client.Send(msg);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Successfully sent email');", true);


                return RedirectToAction("WeeklyDoctorList");
            }
            else
            {
                return RedirectToAction("Index","Home");
            }


        }
        [HttpGet]
        public ActionResult Wrong()
        {
            return PartialView();
        }
    }
}