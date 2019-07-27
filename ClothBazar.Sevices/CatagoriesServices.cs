using ClothBazar.Database;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ClothBazar.Services
{
    public class CatagoriesServices
    {
        #region Singleton
        public static CatagoriesServices Instance
        {
            get
            {
                if (instance == null) instance = new CatagoriesServices();
                return instance;
            }
        }
        private static CatagoriesServices instance { get; set; }
        private CatagoriesServices()
        {
        }
        #endregion
        public void SaveCatagory(Catagory catagory)
        {
            using (var context = new CBContext())
            {
                context.catagories.Add(catagory);
                context.SaveChanges();
            }
        }
        public List<Catagory> GetCatagories(string search,int pageNo)
        {
            int pageSize = 3;

            using (var context = new CBContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.catagories.Where(p => p.name != null && p.name.ToLower()
                          .Contains(search.ToLower()))
                          .OrderBy(x => x.Id)
                          .Skip((pageNo - 1) * pageSize)
                          .Take(pageSize)
                          .Include(x => x.Products)
                          .ToList();
                }
                else
                    return context.catagories
                           .OrderBy(x => x.Id)
                           .Skip((pageNo - 1) * pageSize)
                           .Take(pageSize)
                           .Include(x => x.Products)
                           .ToList();
            }
        }
        public int  GetCatagoriesCount(string search)
        {
            using (var context = new CBContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.catagories.Where(p => p.name != null &&
                    p.name.ToLower().Contains(search.ToLower())).Count();                        
                }
                else  return context.catagories.Count();
            }
        }

        public void SetAuth()
        {
         
        }

        public List<Catagory> GetAllCatagories()
        {
            using (var context = new CBContext())
            {
                return context.catagories.ToList();
            }
        }
        public List<Catagory> GetCatagories()
        {
            using (var context = new CBContext())
            {
                return context.catagories.ToList();
            }
        }
        public List<Catagory> GetFeaturedCatagories()
        {
            using (var context = new CBContext())
            {
                return context.catagories.Where(x=> x.isFeatured && x.ImageURL!=null).ToList();
            }
        }
        public Catagory GetCatagory(int Id)
        {
            using (var context = new CBContext())
            {
                return context.catagories.Find(Id);
            }
        }
       public void Update(Catagory catagory)
        {

            using (var context = new CBContext())
            {
                context.Entry(catagory).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        //public void Delete(Catagory catagory)
        //{
        //    using (var context = new CBContext()) {
        //        context.Entry(catagory).State = System.Data.Entity.EntityState.Deleted;
        //        //context.catagories.Remove(catagory);
        //        context.SaveChanges();
        //    }
        //}
        public void Delete(int Id)
        {
            using (var context = new CBContext())
            {
                var catagory = context.catagories.Where(x => x.Id == Id).Include(x => x.Products).FirstOrDefault();
                context.products.RemoveRange(catagory.Products);
                //context.Entry(catagory).State = System.Data.Entity.EntityState.Deleted;
                context.catagories.Remove(catagory);
                context.SaveChanges();
            }
        }

    }
}