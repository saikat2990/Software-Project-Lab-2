using System;
using System.Collections.Generic;
using System.Text;

namespace ClothBazar.Entities
{
    public class WeeklyDoctorListViewModel
    {
        public int PageNo { get; set; }
        public List<WeeklyDoctorList> WeeklyDoctorLists { get; set; }
        public string SearchItem { get; set; }
        public Pager Pager { get; set; }
    }
}
