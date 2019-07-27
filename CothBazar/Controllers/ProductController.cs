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
    public class ProductController : Controller
    {
        // GET: Product
        //ProductsServices productServices = new ProductsServices();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ProductTable(string search, int? pageNo)
        {
            ProductServicesVeiwModel model = new ProductServicesVeiwModel();

            //model.SearchItem = search;

            model.PageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = ProductsServices.Instance.GetProductsCount(search);

            model.Products = ProductsServices.Instance.GetProducts(model.PageNo);

             //model.Products = ProductsServices.Instance.GetProducts(search, pageNo.Value, 6);

            //model.Pager = new Pager(totalRecords, pageNo, 6);

            if (!string.IsNullOrEmpty(search))
            {
                model.SearchItem = search;
                model.Products = model.Products.Where(p => p.name != null && p.name.Contains(search)).ToList();
            }

            return PartialView(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            //CatagoriesServices catagoriesServices = new CatagoriesServices();
            var catagories = CatagoriesServices.Instance.GetCatagories();
            return PartialView(catagories);
        }
        [HttpPost]
        public ActionResult Create(ProductVeiwModel productVeiwModel)
        {
            // CatagoriesServices catagoriesServices = new CatagoriesServices();

            var product = new Product();
            product.name = productVeiwModel.name;
            product.Description = productVeiwModel.Description;
            product.price = productVeiwModel.price;
            var catagory = CatagoriesServices.Instance.GetCatagory(productVeiwModel.catagoryId);
            product.catagory = catagory;
            product.imgUrl = productVeiwModel.imgUrl;
            ProductsServices.Instance.SaveProduct(product);
            return RedirectToAction("ProductTable");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            EditProductVeiwModel model = new EditProductVeiwModel();

            var product = ProductsServices.Instance.GetProduct(Id);

            model.Id = product.Id;
            model.name = product.name;
            model.Description = product.Description;
            model.price = product.price;
            model.imgUrl = product.imgUrl;
            //model.catagoryId = (Int32)16;
            model.catagoryId =  product.catagoryId;
            model.AvailableCatagory = CatagoriesServices.Instance.GetCatagories();
            //product.catagory != null ? product.catagory.Id : 0;

            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Edit(EditProductVeiwModel editProductVeiwModel)
        {
            var product = ProductsServices.Instance.GetProduct(editProductVeiwModel.Id);

            if (!string.IsNullOrEmpty(editProductVeiwModel.imgUrl)) {
                product.imgUrl = editProductVeiwModel.imgUrl;
            }
            
            product.catagory = null;
            //CatagoriesServices.Instance.GetCatagory(editProductVeiwModel.catagoryId);
            product.name = editProductVeiwModel.name;
            product.price = editProductVeiwModel.price;
            product.catagoryId = editProductVeiwModel.catagoryId;
            product.Description = editProductVeiwModel.Description;
            ProductsServices.Instance.UpdateProduct(product);
            return RedirectToAction("ProductTable");


        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            var product = ProductsServices.Instance.GetProduct(Id);
            ProductsServices.Instance.DeleteProduct(product);
            return RedirectToAction("ProductTable");
        }
        [HttpGet]
        public ActionResult Details(int ID)
        {
            ProductDetailsModels model = new ProductDetailsModels();

            model.Product = ProductsServices.Instance.GetProduct(ID);

            if (model.Product == null) return HttpNotFound();

            return View(model);
        }


    }
}

//{
//    public class ProductController : Controller
//    {
//        // GET: Product
//        public ActionResult Index()
//        {
//            return View();
//        }

//        public ActionResult ProductTable(string search, int? pageNo)
//        {
//            var pageSize = ConfigurationServices.Instance.PageSize();

//            ProductSearchViewModel model = new ProductSearchViewModel();
//            model.SearchTerm = search;

//            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

//            var totalRecords = ProductsServices.Instance.GetProductsCount(search);
//            model.Products = ProductsServices.Instance.GetProducts(search, pageNo.Value, pageSize);

//            model.Pager = new Pager(totalRecords, pageNo, pageSize);

//            return PartialView(model);
//        }

//        [HttpGet]
//        public ActionResult Create()
//        {
//            NewProductViewModel model = new NewProductViewModel();

//            model.AvailableCategories = CatagoriesServices.Instance.GetAllCatagories();

//            return PartialView(model);
//        }

//        [HttpPost]
//        public ActionResult Create(NewProductViewModel model)
//        {
//            var newProduct = new Product();
//            newProduct.name = model.Name;
//            newProduct.Description = model.Description;
//            newProduct.price = model.Price;
//            newProduct.catagory = CatagoriesServices.Instance.GetCatagory(model.CategoryID);
//            newProduct.imgUrl = model.ImageURL;

//            ProductsServices.Instance.SaveProduct(newProduct);
//            return RedirectToAction("ProductTable");
//        }

//        [HttpGet]
//        public ActionResult Edit(int ID)
//        {
//            EditProductVeiwModel model = new EditProductVeiwModel();

//            var product = ProductsServices.Instance.GetProduct(ID);

//            model.Id = product.Id;
//            model.name = product.name;
//            model.Description = product.Description;
//            model.price = product.price;
//            model.catagoryId = product.catagory != null ? product.catagory.Id : 0;
//            model.imgUrl = product.imgUrl;

//            model.AvailableCatagory = CatagoriesServices.Instance.GetAllCatagories();

//            return PartialView(model);
//        }

//        [HttpPost]
//        public ActionResult Edit(EditProductVeiwModel model)
//        {
//            var existingProduct = ProductsServices.Instance.GetProduct(model.Id);
//            existingProduct.name = model.name;
//            existingProduct.Description = model.Description;
//            existingProduct.price = model.price;

//            existingProduct.catagory = null; //mark it null. Because the referncy key is changed below
//            existingProduct.catagoryId = model.catagoryId;

//            //dont update imageURL if its empty
//            if (!string.IsNullOrEmpty(model.imgUrl))
//            {
//                existingProduct.imgUrl = model.imgUrl;
//            }

//            ProductsServices.Instance.UpdateProduct(existingProduct);

//            return RedirectToAction("ProductTable");
//        }

//        [HttpPost]
//        public ActionResult Delete(int ID)
//        {
//            ProductsServices.Instance.DeleteProduct(ProductsServices.Instance.GetProduct(ID));

//            return RedirectToAction("ProductTable");
//        }

//        [HttpGet]
//        public ActionResult Details(int ID)
//        {
//            ProductDetailsModels model = new ProductDetailsModels();

//            model.Product = ProductsServices.Instance.GetProduct(ID);

//            if (model.Product == null) return HttpNotFound();

//            return View(model);
//        }
//    }
//}