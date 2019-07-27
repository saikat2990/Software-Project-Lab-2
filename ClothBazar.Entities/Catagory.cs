using System;
using System.Collections.Generic;
using System.Text;

namespace ClothBazar.Entities
{
    public class Catagory:BaseEntity
    {
        public string ImageURL { get; set; }
        public List<Product> Products { get; set; }
        public bool isFeatured { get; set; }
    }
}
