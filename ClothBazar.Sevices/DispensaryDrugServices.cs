using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothBazar.Database;
using ClothBazar.Entities;

namespace ClothBazar.Sevices
{
    public class DispensaryDrugServices
    {
        #region Singleton
        public static DispensaryDrugServices Instance
        {
            get
            {
                if (instance == null) instance = new DispensaryDrugServices();
                return instance;
            }
        }
        private static DispensaryDrugServices instance { get; set; }
        private DispensaryDrugServices()
        {

        }
        #endregion
        public List<DispensaryDrugTable> GetDrugs(int pageNo)
        {

            int pageSize = 6;

            using (var context = new CBContext())
            {
                return context.DispensaryDrugTables.OrderByDescending(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
                // return context.products.Include(x => x.catagory).ToList();

            }
            
        }
        public void SaveService(DispensaryDrugTable dispensaryDrugTable)
        {

            using (var context = new CBContext())
            {
                context.DispensaryDrugTables.Add(dispensaryDrugTable);
                context.SaveChanges();
            }
            
        }
        public DispensaryDrugTable GetDispensaryDrugServices(int Id)
        {
            using (var context = new CBContext())
            {
                return context.DispensaryDrugTables.Find(Id);
            }
        }
        public DispensaryDrugTable GetDispensaryDrugServices(string name)
        {
            using (var context = new CBContext())
            {
                List<DispensaryDrugTable>s= context.DispensaryDrugTables.Where(x=> x.Name.ToLower().Contains(name.ToLower())).ToList();
                return s[0];
            }
        }

        public void Update(DispensaryDrugTable edit)
        {
            using (var context = new CBContext())
            {
                context.Entry(edit).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public DispensaryDrugTable GetDispensaryDrug(int id)
        {
            using (var context = new CBContext())
            {
                return context.DispensaryDrugTables.Find(id);
            }
        }

        public void DeleteProduct(DispensaryDrugTable product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Deleted;
                //context.catagories.Remove(catagory);
                context.SaveChanges();
            }
        }

        public List<DispensaryDrugTable> GetDispensaryAllDrug()
        {
            using (var context = new CBContext())
            {
                return context.DispensaryDrugTables.ToList();
            }
        }


    }
}
