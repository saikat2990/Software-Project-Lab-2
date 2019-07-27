using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CothBazar.VeiwModel
{
    public class HomeViewModel
    {
        public List<Catagory> catagories { get; set; }
        public List<Product> products { get; set; }
    }
}