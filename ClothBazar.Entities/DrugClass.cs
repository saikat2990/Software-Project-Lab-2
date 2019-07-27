using System;
using System.Collections.Generic;
using System.Text;

namespace ClothBazar.Entities
{
    public class Requestclass
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public List<DrugRequest> Drugs { get; set; }
    }


}
