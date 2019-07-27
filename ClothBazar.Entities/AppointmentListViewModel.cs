using System;
using System.Collections.Generic;
using System.Text;

namespace ClothBazar.Entities
{
    public class AppointmentListViewModel
    {

        public int PageNo { get; set; }
        public List<AppointmentList> AppointmentLists { get; set; }
        public string SearchItem { get; set; }
        public Pager Pager { get; set; }

        
    }
}
