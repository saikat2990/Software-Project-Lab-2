using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClothBazar.Entities
{
    public class AppointmentList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [MaxLength(500)]
        public string Problem { get; set; }
        public string DoctorName { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public int SerialNum { get; set; }
    }
}
