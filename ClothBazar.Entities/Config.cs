using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClothBazar.Entities
{
    public class Config
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }


    }
}
