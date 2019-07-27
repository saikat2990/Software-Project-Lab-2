using ClothBazar.Database;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Sevices
{
    public class DispensariesServices
    {

        #region Singleton
        public static DispensariesServices Instance
        {
            get
            {
                if (instance == null) instance = new DispensariesServices();
                return instance;
            }
        }
        private static DispensariesServices instance { get; set; }
        private DispensariesServices()
        {

        }
        #endregion
        public decimal GetCost(string Name,int amount) {

            using (var context = new CBContext()){

                return context.Drugs.Find(Name).PricePerTab*amount;
                

            }
        }

        public List<DispensaryServices> GetDispensaryDrugs(int pageNo)
        {
            int pageSize = 6;

            using (var context = new CBContext())
            {
                return context.DispensaryServices.OrderByDescending(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
                // return context.products.Include(x => x.catagory).ToList();

            }
        }

        public void SaveService(DispensaryServices dispensaryServices)
        {
            using (var context = new CBContext())
            {
               
                context.DispensaryServices.Add(dispensaryServices);
                context.SaveChanges();
            }
        }

        public DispensaryServices GetDispensaryService(int id)
        {
            using (var context = new CBContext())
            {
                return context.DispensaryServices.Find(id);
            }
        }

        public void Update(DispensaryServices dispensaryServices)
        {
            using (var context = new CBContext())
            {
                context.Entry(dispensaryServices).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteProduct(DispensaryServices product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Deleted;
                //context.catagories.Remove(catagory);
                context.SaveChanges();
            }
        }

        public object GetDispensaryDrug(int id)
        {
            throw new NotImplementedException();
        }

        public List<DispensaryServices> GetDispensaryService(string name, string email, int serial)
        {
            using (var context = new CBContext()){
                return context.DispensaryServices.Where(p => p.Name.ToLower().Contains(name.ToLower()) &&
                p.Email.ToLower().Contains(email.ToLower()) && p.SerialNum == serial).ToList();
            }
        }
    }
}
