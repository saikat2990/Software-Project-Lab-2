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
    public class AppoinmentListsServices
    {
       #region Singleton
        public static AppoinmentListsServices Instance
        {
            get
            {
                if (instance == null) instance = new AppoinmentListsServices();
                return instance;
            }
        }
        private static AppoinmentListsServices instance { get; set; }
        private AppoinmentListsServices()
        {

        }
        #endregion



        public List<AppointmentList> GetAppiontmentLists(int pageNo)
        {
            int pageSize = 6;

            using (var context = new CBContext())
            {
                return context.AppointmentList.OrderByDescending(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
                // return context.products.Include(x => x.catagory).ToList();

            }
        }

        public void SaveService(AppointmentList dispensaryServices)
        {
            using (var context = new CBContext())
            {

                context.AppointmentList.Add(dispensaryServices);
                context.SaveChanges();
            }
        }

        public AppointmentList GetAppointmentList(int id)
        {
            using (var context = new CBContext())
            {
                return context.AppointmentList.Find(id);
            }
        }

        public void Update(AppointmentList appointmentList)
        {
            using (var context = new CBContext())
            {
                context.Entry(appointmentList).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteProduct(AppointmentList product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Deleted;
                //context.catagories.Remove(catagory);
                context.SaveChanges();
            }
        }
    }
}
