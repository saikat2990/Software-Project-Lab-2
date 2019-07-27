using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Database
{
    public class CBContext:DbContext,IDisposable
    {
        public CBContext(): base("dbconnection")
        {
                
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catagory>().Property(p => p.name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Product>().Property(p => p.name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Student>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Drug>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        }
        public DbSet<Product> products { get; set; }
        public DbSet<Catagory> catagories { get; set; }
        public DbSet<Config> configurations { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<StudentServices> StudentServices { get; set; }
        public DbSet<DispensaryServices> DispensaryServices { get; set; }
        public DbSet<AppointmentList> AppointmentList { get; set; }
        public DbSet<WeeklyDoctorList> WeeklyDoctorList { get; set; }
        public DbSet<DrugRequest> DrugRequests { get; set; }
        public DbSet<Requestclass> DrugClasses { get; set; }
        public DbSet<DispensaryDrugTable> DispensaryDrugTables { get; set; }
       
    }
}
