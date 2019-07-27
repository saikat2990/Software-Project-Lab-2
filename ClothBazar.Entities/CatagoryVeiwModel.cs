using System;
using System.Collections.Generic;
using System.Text;

namespace ClothBazar.Entities
{
    public class CatagoryVeiwModel
    {
        
        public string name { get; set; }
        public string Description { get; set; }
        public decimal price { get; set; }

        public int catagoryId { get; set; }

    }

    public class CatagorySearchViewModel
    {
        public List<Catagory> Catagories { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
        public int pageNo { get; set; }
    }

}
