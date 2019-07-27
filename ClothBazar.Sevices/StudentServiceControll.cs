using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ClothBazar.Database;
using ClothBazar.Entities;

namespace ClothBazar.Sevices
{
    public class StudentServiceControll
    {
        #region Singleton
        public static StudentServiceControll Instance
        {
            get
            {
                if (instance == null) instance = new StudentServiceControll();
                return instance;
            }
        }
        private static StudentServiceControll instance { get; set; }
        private StudentServiceControll()
        {
        }
        #endregion


        public int GetStudentsCount(string search)
        {
            using (var context = new CBContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Students.Where(product => product.Name != null &&
                         product.Name.ToLower().Contains(search.ToLower()))
                         .Count();
                }
                else
                {
                    return context.Students.Count();
                }
            }
        }

        public List<Student> GetStudents(int pageNo)
        {
            int pageSize = 6;

            using (var context = new CBContext())
            {
                return context.Students.OrderByDescending(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
                // return context.products.Include(x => x.catagory).ToList();

            }
        }

        public void SaveProduct(Student product)
        {
            using (var context = new CBContext())
            {
               // context.Entry(product.catagory).State = System.Data.Entity.EntityState.Unchanged;
                context.Students.Add(product);
                context.SaveChanges();
            }
        }

        public Student GetStudent(int Id)
        {
            using (var context = new CBContext())
            {
                return context.Students.Find(Id);
            }
        }

        public void UpdateStudent(Student product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteStudent(Student product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Deleted;
                //context.catagories.Remove(catagory);
                context.SaveChanges();
            }
        }

        public bool IsRemain(string Name, string Password)
        {
            using (var context = new CBContext()) {
                var student =context.Students.Where(product => product.Name != null &&
                         product.Name.ToLower().Contains(Name.ToLower())).ToList();
                foreach (var s in student) 
                {
                    if (s.Password == Password) {
                        return true;
                    }
                }
            }

            return false;


        }
    }
}
