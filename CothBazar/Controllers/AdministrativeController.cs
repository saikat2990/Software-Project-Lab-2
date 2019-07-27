using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Entities;
using ClothBazar.Sevices;

namespace CothBazar.Controllers
{
    public class AdministrativeController : Controller
    {
        // GET: Administrative
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AppointmentList(string search, int? pageNo)
        {

            AppointmentListViewModel model = new AppointmentListViewModel();

            //model.SearchItem = search;

            model.PageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            // var totalRecords = DispensariesServices.Instance.GetDispensaryCount(search);

            model.AppointmentLists = AppoinmentListsServices .Instance.GetAppiontmentLists(model.PageNo);

            //model.Products = ProductsServices.Instance.GetProducts(search, pageNo.Value, 6);

            //model.Pager = new Pager(totalRecords, pageNo, 6);

            if (!string.IsNullOrEmpty(search))
            {
                model.SearchItem = search;
                model.AppointmentLists = model.AppointmentLists.Where(p => p.Name != null && p.Name.Contains(search)).ToList();
            }

            return PartialView(model);
        }

        [HttpGet]
        public ActionResult Create()
        {

            AppointmentList appointmentList = new AppointmentList();
            return PartialView(appointmentList);
        }

        [HttpPost]
        public ActionResult Create(AppointmentList appoinmentList)
        {

            AppoinmentListsServices.Instance.SaveService(appoinmentList);
            return RedirectToAction("AppointmentList");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            AppointmentList appoinmentList = new AppointmentList();
            appoinmentList = AppoinmentListsServices.Instance.GetAppointmentList(Id);
            return PartialView(appoinmentList);

        }

        [HttpPost]
        public ActionResult Edit(AppointmentList appoinmentList)
        {

            AppointmentList edit = AppoinmentListsServices.Instance.GetAppointmentList(appoinmentList.Id);

            edit.Date = appoinmentList.Date;
            edit.DoctorName = appoinmentList.DoctorName;
            edit.Email = appoinmentList.Email;
            edit.Name = appoinmentList.Name;
            edit.Problem = appoinmentList.Problem;
            edit.SerialNum = appoinmentList.SerialNum;
            edit.Time = appoinmentList.Time;

            AppoinmentListsServices.Instance.Update(edit);
            return RedirectToAction("AppointmentList");
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            var product = AppoinmentListsServices.Instance.GetAppointmentList(Id);
            AppoinmentListsServices.Instance.DeleteProduct(product);
            return RedirectToAction("AppointmentList");
        }
    }
}