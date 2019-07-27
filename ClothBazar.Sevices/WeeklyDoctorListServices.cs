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
    public class WeeklyDoctorListServices
    {
        #region Singleton
        public static WeeklyDoctorListServices Instance
        {
            get
            {
                if (instance == null) instance = new WeeklyDoctorListServices();
                return instance;
            }
        }
        private static WeeklyDoctorListServices instance { get; set; }
        private WeeklyDoctorListServices()
        {

        }
        #endregion
        public List<WeeklyDoctorList> GetWeeklyDoctorLists(int pageNo)
        {
            int pageSize = 6;

            using (var context = new CBContext())
            {
                return context.WeeklyDoctorList.OrderByDescending(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
                // return context.products.Include(x => x.catagory).ToList();

            }
        }
        public void SaveService(WeeklyDoctorList weeklyDoctorList)
        {
            using (var context = new CBContext())
            {

                context.WeeklyDoctorList.Add(weeklyDoctorList);
                context.SaveChanges();
            }
        }
        public WeeklyDoctorList GetWeeklyDoctorList(int id)
        {
            using (var context = new CBContext())
            {
                return context.WeeklyDoctorList.Find(id);
            }
        }
        public void Update(WeeklyDoctorList weeklyDoctorList)
        {
            using (var context = new CBContext())
            {
                context.Entry(weeklyDoctorList).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteProduct(WeeklyDoctorList product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Deleted;
                //context.catagories.Remove(catagory);
                context.SaveChanges();
            }
        }
        public List<WeeklyDoctorList> GetWeeklyDoctorListRound(string search)
        {
            using (var context = new CBContext())
            {

              return context.WeeklyDoctorList.Where(it => it.DoctorName.ToLower().Contains(search.ToLower())).ToList();

            }
        }
        public List<WeeklyDoctorList> GetAppointedDortorList(string search)
        {
            using (var context = new CBContext())
            {

                return context.WeeklyDoctorList.ToList();
                //ToLower().Contains(search.ToLower()))
            }
        }
        public object GetCount()
        {
            using (var context = new CBContext())
            {

                return context.WeeklyDoctorList.Count();

            }
        }
        public bool IsRemain(AppointmentList appoinmentList)
        {
            using (var context = new CBContext())
            {

               var val = context.WeeklyDoctorList.ToList();

                foreach (var v in val)
                {
                    if (v.Date == appoinmentList.Date && v.DoctorName == appoinmentList.DoctorName)
                    {
                        return true;
                    }

                }

            }
            return false;

        }
        public WeeklyDoctorList GetSerialNum(AppointmentList appoinmentList)
        {
            using (var context = new CBContext())
            {

                var val = context.WeeklyDoctorList.ToList();

                foreach (var v in val)
                {
                    if (v.Date == appoinmentList.Date && v.DoctorName == appoinmentList.DoctorName)
                    {
                        return v;
                    }

                }

            }
            return null;
        }
    }
}
