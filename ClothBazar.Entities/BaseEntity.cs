using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClothBazar.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5)][MaxLength(50)]
        public string name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}
