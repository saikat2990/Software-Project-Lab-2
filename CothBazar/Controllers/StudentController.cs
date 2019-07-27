using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Sevices;
using System.Data.Entity;


namespace CothBazar.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult StudentTable(String search, int? pageNo)
        {
            StudentServicesVIewModel model = new StudentServicesVIewModel();

            //model.SearchItem = search;

            model.PageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = StudentServiceControll.Instance.GetStudentsCount(search);

            model.Students = StudentServiceControll.Instance.GetStudents(model.PageNo);

            // model.Products = ProductsServices.Instance.GetProducts(search, pageNo.Value, 6);

            //model.Pager = new Pager(totalRecords, pageNo, 6);

            if (!string.IsNullOrEmpty(search))
            {
                model.SearchItem = search;
                model.Students = model.Students.Where(p => p.Name != null && p.Name.Contains(search)).ToList();
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
        public ActionResult Create(Student student)
        {
            // CatagoriesServices catagoriesServices = new CatagoriesServices();

            var product = new Student();
            product.Name = student.Name;
            product.ClassRoll = student.ClassRoll;
            product.Dpt = student.Dpt;
            product.Email = student.Email;
            product.ImgUrl = student.ImgUrl;
            product.Password = student.Password;
            product.RegNo = student.RegNo;
            StudentServiceControll.Instance.SaveProduct(product);
            return RedirectToAction("StudentTable");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            Student model = new Student();

            var student = StudentServiceControll.Instance.GetStudent(Id);

            model.Id = student.Id;
            model.Name = student.Name;
            model.RegNo = student.RegNo;
            model.Password = student.Password;
            model.ClassRoll = student.ClassRoll;
            model.ImgUrl = student.ImgUrl;
            model.Dpt = student.Dpt;
            model.Email = student.Email;


            //product.catagory != null ? product.catagory.Id : 0;

            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            var product = StudentServiceControll.Instance.GetStudent(student.Id);

            if (!string.IsNullOrEmpty(student.ImgUrl))
            {
                product.ImgUrl = student.ImgUrl;
            }

            product.Name = student.Name;
            product.RegNo = student.RegNo;
            product.Password = student.Password;
            product.ClassRoll = student.ClassRoll;
            product.ImgUrl = student.ImgUrl;
            product.Dpt = student.Dpt;
            product.Email = student.Email;

            StudentServiceControll.Instance.UpdateStudent(product);
            return RedirectToAction("StudentTable");

        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            var product = StudentServiceControll.Instance.GetStudent(Id);
            StudentServiceControll.Instance.DeleteStudent(product);
            return RedirectToAction("StudentTable");
        }
        [HttpGet]
        public ActionResult LogIn() {
            return View();
        }
        [HttpGet]
        public ActionResult Table()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Table(string Name, string Email, string Password)
        //{

        //    if (StudentServiceControll.Instance.IsRemain(Name, Email, Password))
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    else {
        //        return RedirectToAction("Reg","Student");
        //    }
        //    return RedirectToAction("Reg");
        //}
        [HttpGet]
        public ActionResult Reg() {
            return View();
        }


    }
}