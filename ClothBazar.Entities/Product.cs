using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClothBazar.Entities
{
    public class Product : BaseEntity
    {

        public virtual Catagory catagory { get; set; }
        public string imgUrl { get; set; }
        public int catagoryId { get; set; }

        [Range(1,1000000)]
        public decimal price { get; set; }
    }
}
