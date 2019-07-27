using System;
using System.Collections.Generic;
using System.Text;

namespace ClothBazar.Entities
{
    public class DispensaryVeiwModel
    {
        public int PageNo { get; set; }
        public List<DispensaryServices> DispensaryServices { get; set; }
        public string SearchItem { get; set; }
        public Pager Pager { get; set; }

    }
    public class DrugList
    {
        public string Name { get; set; }
        public int amount { get; set; }
    }
}
