using System;
using System.Collections.Generic;
using System.Text;

namespace ClothBazar.Entities
{
    public class StudentServicesVIewModel
    {
        public int PageNo { get; set; }
        public List<Student> Students { get; set; }
        public string SearchItem { get; set; }
        public Pager Pager { get; set; }
    }

    public class DrugServicesVeiwModel
    {
        public int PageNo { get; set; }
        public List<Drug> Drugs { get; set; }
        public string SearchItem { get; set; }
        public Pager Pager { get; set; }

    }
}
