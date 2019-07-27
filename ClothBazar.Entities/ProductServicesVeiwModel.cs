using System;
using System.Collections.Generic;
using System.Text;

namespace ClothBazar.Entities
{
    public class ProductServicesVeiwModel
    {
        public int PageNo { get; set; }
        public List<Product> Products { get; set; }
        public string SearchItem { get; set; }
        public Pager Pager { get; set; }
    }
}
