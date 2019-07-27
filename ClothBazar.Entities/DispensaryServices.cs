using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClothBazar.Entities
{
    public class DispensaryServices
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int SerialNum { get; set; }
        public string Problem { get; set; }
        public string Date { get; set; }
        public decimal TotalCost { get; set; }
        public string DoctorName { get; set; }
        [MaxLength(500)]
        public string DrugService { get; set; }
    }
}
