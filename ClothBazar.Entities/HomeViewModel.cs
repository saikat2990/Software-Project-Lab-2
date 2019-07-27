using System;
using System.Collections.Generic;
using System.Text;

namespace ClothBazar.Entities
{
    public class HomeViewModel
    {
        public List<Catagory> FeaturedCatagories { get; set; }
        public List<Product> FeaturedProducts { get; set; }
        public int ifTrue { get; set; }
    }
}
