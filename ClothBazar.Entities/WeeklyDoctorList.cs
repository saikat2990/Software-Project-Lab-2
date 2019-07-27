using System;
using System.Collections.Generic;
using System.Text;

namespace ClothBazar.Entities
{
    public class WeeklyDoctorList
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public string DoctorName { get; set; }
        public int Serial { get; set; }
        public string Date { get; set; }
    }
}
