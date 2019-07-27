using System;
using System.Collections.Generic;
using System.Text;

namespace ClothBazar.Entities
{
    public class ProductVeiwModel
    {
        public string name { get; set; }
        public string Description { get; set; }
        public decimal price { get; set; }

        public int catagoryId { get; set; }
        public string imgUrl { get; set; }
        public Pager Pager { get; set; }
    }

    public class NewProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public string ImageURL { get; set; }

        public List<Catagory> AvailableCategories { get; set; }
    }

    public class EditProductVeiwModel
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string Description { get; set; }
        public decimal price { get; set; }
        public List<Catagory> AvailableCatagory { get; set; }
        public int catagoryId { get; set; }
        public string imgUrl { get; set; }
    }

    public class ProductSearchViewModel
    {
        public List<Product> Products { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }

    public class ProductDetailsModels
    {
        public Product Product { get; set; }
    }
}
