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
    public class ShopController : Controller
    {
        //ProductsServices productsServices = new ProductsServices();
        public ActionResult Index(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            var pageSize = ConfigurationServices.Instance.ShopPageSize();

            ShopViewModel model = new ShopViewModel();

            model.SearchTerm = searchTerm;
            model.FeaturedCategories = CatagoriesServices.Instance.GetFeaturedCatagories();
            model.MaximumPrice = ProductsServices.Instance.GetMaximumPrice();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            model.SortBy = sortBy;
            model.CategoryID = categoryID;

            int totalCount = ProductsServices.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);
            model.Products = ProductsServices.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value, pageSize);

            model.Pager = new Pager(totalCount, pageNo, pageSize);

            return View(model);
        }

        public ActionResult FilterProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            var pageSize = ConfigurationServices.Instance.ShopPageSize();

            FilterProductsViewModel model = new FilterProductsViewModel();

            model.SearchTerm = searchTerm;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.SortBy = sortBy;
            model.CategoryID = categoryID;

            int totalCount = ProductsServices.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);
            model.Products = ProductsServices.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value, pageSize);

            model.Pager = new Pager(totalCount, pageNo, pageSize);

            return PartialView(model);
        }



        public ActionResult Checkout()
        {
            var CartProductsCookie = Request.Cookies["CartDrugs"];

            CheckViewModel checkViewModel = new CheckViewModel();
            if (CartProductsCookie!=null) {

                //var productIds = CartProductsCookie.Value;

                //var ids = productIds.Split('-');

                //List<int> pIds = ids.Select(x=> int.Parse(x)).ToList();

                checkViewModel.cartProductds = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                checkViewModel.cartProducts = ProductsServices.Instance.GetProducts(checkViewModel.cartProductds);
            }
            return View(checkViewModel);
        }
    }
}