using System;
using System.Collections.Generic;
using System.Text;

namespace ClothBazar.Entities
{
    public class DispensaryDrugTableViewModel
    {
        public int PageNo { get; set; }
        public List<DispensaryDrugTable> DispensaryDrugs { get; set; }
        public string SearchItem { get; set; }
        public Pager Pager { get; set; }
    }
}
