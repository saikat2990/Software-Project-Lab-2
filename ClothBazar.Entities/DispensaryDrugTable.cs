using System;
using System.Collections.Generic;
using System.Text;

namespace ClothBazar.Entities
{
    public class DispensaryDrugTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal ServiceAmount { get; set; }
        public decimal RemainAmount { get; set; }
        public decimal StafftSeriveAmount { get; set; }
        public decimal PricePerTab { get; set; }
        public string Description { get; set; }
        public string ProductionDate { get; set; }
        public string ExpireDate { get; set; }
       
    }
}
