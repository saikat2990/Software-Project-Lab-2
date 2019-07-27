using ClothBazar.Database;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Sevices
{
    public class ConfigurationServices
    {
        #region Singleton
        public static ConfigurationServices Instance
        {
            get
            {
                if (instance == null) instance = new ConfigurationServices();
                return instance;
            }
        }
        private static ConfigurationServices instance { get; set; }

        private ConfigurationServices()
        {
        }
        #endregion


        public Config GetConfig(string Key)
        {
            using (var context = new CBContext())
            {
                return context.configurations.Find(Key);
            }
        }

        public int PageSize()
        {
            using (var context = new CBContext())
            {
               // var pageSizeConfig = context.configurations.Find("PageSize");

                return 5;
                //pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 
            }
        }

        public int ShopPageSize()
        {
            using (var context = new CBContext())
            {
                // var pageSizeConfig = context.configurations.Find("ShopPageSize");

                // return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 6;
                return 6;
            }
        }

        //public Config GetConfig(int Id) {
        //    using (var context = new CBContext())
        //    {
        //        return context.configurations.Find(Id);
        //    }
        //}
    }
}
