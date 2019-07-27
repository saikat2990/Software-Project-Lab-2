using System;
using System.Collections.Generic;
using System.Text;

namespace ClothBazar.Entities
{
    public class StudentServices
    {
        public int Id { get; set; }
        public virtual Student Student { get; set; }
        public int StudentId { get; set; }
        public List<Drug> StudentDrug { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
