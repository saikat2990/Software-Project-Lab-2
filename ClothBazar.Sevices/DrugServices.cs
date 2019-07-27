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
    public class DrugServices
    {

        #region Singleton
        public static DrugServices Instance
        {
            get
            {
                if (instance == null) instance = new DrugServices();
                return instance;
            }
        }
        private static DrugServices instance { get; set; }
        private DrugServices()
        {
        }
        #endregion
        public int GetDrugsCount(string search)
        {
            using (var context = new CBContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Drugs.Where(product => product.Name != null &&
                         product.Name.ToLower().Contains(search.ToLower()))
                         .Count();
                }
                else
                {
                    return context.Drugs.Count();
                }
            }
        }
        public List<Drug> GetDrugs(int pageNo)
        {
            int pageSize = 6;

            using (var context = new CBContext())
            {
                return context.Drugs.OrderByDescending(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
                // return context.products.Include(x => x.catagory).ToList();

            }
        }
        public List<Drug> GetDrugs()
        {
            int pageSize = 6;

            using (var context = new CBContext())
            {
                return context.Drugs.OrderByDescending(x => x.Id).ToList();
                // return context.products.Include(x => x.catagory).ToList();

            }
        }
        public void SaveDrug(Drug product)
        {
            using (var context = new CBContext())
            {
                // context.Entry(product.catagory).State = System.Data.Entity.EntityState.Unchanged;
                context.Drugs.Add(product);
                context.SaveChanges();
            }
        }
        public Drug GetDrug(int Id)
        {
            using (var context = new CBContext())
            {
                return context.Drugs.Find(Id);
            }
        }
        public Drug GetDrug(string name)
        {
            using (var context = new CBContext())
            {
                List<Drug>drug = context.Drugs.Where(x=>x.Name.ToLower().Contains(name.ToLower())).ToList();
                return drug[0];
            }
        }
        public void UpdateDrug(Drug product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteDrug(Drug product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Deleted;
                //context.catagories.Remove(catagory);
                context.SaveChanges();
            }
        }
        public Requestclass GEtAllDrug()
        {


            List<Drug> drugs = new List<Drug>();

            using (var context = new CBContext()) {
                drugs = context.Drugs.ToList();
            }

            Requestclass r = new Requestclass();
            List<DrugRequest> drugRequests = new List<DrugRequest>();
            foreach (Drug drug in drugs) {
                drugRequests.Add(new DrugRequest { DrugName = drug.Name, Amount = 0 });
            }
            r.Drugs = drugRequests;
            return r;
        }
        public void SaveDrugClass(Requestclass drugClass)
        {
            using (var context = new CBContext())
            {
                context.DrugClasses.Add(drugClass);
                context.SaveChanges();
            }
        }
    }
}

