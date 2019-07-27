using System;
using System.Collections.Generic;
using System.Text;

namespace ClothBazar.Entities
{
    public class Drug
    {
        public int     Id { get; set; }
        public string  Name { get; set; }
        public decimal DespensaryAmount { get; set; }
        public decimal StoreHouseAmount { get; set; }
        public decimal StudentSeriveAmount { get; set; }
        public decimal StafftSeriveAmount { get; set; }
        public decimal PricePerTab { get; set; }
        public string  Description { get; set; }
        public string  ProductionDate { get; set; }
        public string  ExpireDate { get; set; }
        public int ServiceAmount { get; set; }

    }
}
