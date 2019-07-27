using ClothBazar.Database;
using ClothBazar.Entities;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Services
{
    public class ProductsServices
    {

        #region Singleton
        public static ProductsServices Instance {
            get
            {
                if (instance == null) instance = new ProductsServices();
                return instance;
            }
        }
        private static ProductsServices instance { get; set; } 
        private ProductsServices()
        {
        }
        #endregion


        public List<Product> SearchProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int pageNo, int pageSize)
        {
            using (var context = new CBContext())
            {
                var products = context.products.ToList();

                if (categoryID.HasValue)
                {
                    products = products.Where(x => x.catagory.Id == categoryID.Value).ToList();
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x => x.name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }

                if (minimumPrice.HasValue)
                {
                    products = products.Where(x => x.price >= minimumPrice.Value).ToList();
                }

                if (maximumPrice.HasValue)
                {
                    products = products.Where(x => x.price <= maximumPrice.Value).ToList();
                }

                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            products = products.OrderByDescending(x => x.Id).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(x => x.price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(x => x.price).ToList();
                            break;
                    }
                }

                return products.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }
        public int SearchProductsCount(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy)
        {
            using (var context = new CBContext())
            {
                var products = context.products.ToList();

                if (categoryID.HasValue)
                {
                    products = products.Where(x => x.catagory.Id== categoryID.Value).ToList();
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x => x.name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }

                if (minimumPrice.HasValue)
                {
                    products = products.Where(x => x.price >= minimumPrice.Value).ToList();
                }

                if (maximumPrice.HasValue)
                {
                    products = products.Where(x => x.price <= maximumPrice.Value).ToList();
                }

                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            products = products.OrderByDescending(x => x.Id).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(x => x.price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(x => x.price).ToList();
                            break;
                    }
                }

                return products.Count;
            }
        }
        public int GetMaximumPrice()
        {
            using (var context = new CBContext())
            {
                return (int)(context.products.Max(x => x.price));
            }
        }
        public Product GetProduct(int Id)
        {
            using (var context = new CBContext())
            {
                return context.products.Find(Id);
            }
        }
        public List<Product> GetProducts(List<int> Ids)
        {
            using (var context = new CBContext())
            {
                return context.products.Where(product => Ids.Contains(product.Id)).ToList();
            }
        }
        public List<Product> GetProducts(int pageNo)
        {
            int pageSize = 6;

            using (var context = new CBContext())
            {
                return context.products.OrderByDescending(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.catagory).ToList();
                // return context.products.Include(x => x.catagory).ToList();

            }
        }
        public List<Product> GetProducts(int pageNo, int pageSize)
        {
            using (var context = new CBContext())
            {
                return context.products.OrderByDescending(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.catagory).ToList();
            }
        }
        public List<Product> GetProducts(string search, int pageNo, int pageSize)
        {
            using (var context = new CBContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.products.Where(product => product.name != null &&
                         product.name.ToLower().Contains(search.ToLower()))
                         .OrderBy(x => x.Id)
                         .Skip((pageNo - 1) * pageSize)
                         .Take(pageSize)
                         .Include(x => x.catagory)
                         .ToList();
                }
                else
                {
                    return context.products
                        .OrderBy(x => x.Id)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.catagory)
                        .ToList();
                }
            }
        }
        public int GetProductsCount(string search)
        {
            using (var context = new CBContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.products.Where(product => product.name != null &&
                         product.name.ToLower().Contains(search.ToLower()))
                         .Count();
                }
                else
                {
                    return context.products.Count();
                }
            }
        }
        public List<Product> GetProductsByCategory(int categoryID, int pageSize)
        {
            using (var context = new CBContext())
            {
                return context.products.Where(x => x.catagory.Id == categoryID).OrderByDescending(x => x.Id).Take(pageSize).Include(x => x.catagory).ToList();
            }
        }
        public List<Product> GetLatestProducts(int numberOfProducts)
        {
            using (var context = new CBContext())
            {
                return context.products.OrderByDescending(x => x.Id).Take(numberOfProducts).Include(x => x.catagory).ToList();
            }
        }
        public void SaveProduct(Product product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product.catagory).State = System.Data.Entity.EntityState.Unchanged;
                context.products.Add(product);
                context.SaveChanges();
            }
        }
        public void UpdateProduct(Product product)
        {

            using (var context = new CBContext())
            {
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteProduct(Product product)
        {
            using (var context = new CBContext()) {
                context.Entry(product).State = System.Data.Entity.EntityState.Deleted;
                //context.catagories.Remove(catagory);
                context.SaveChanges();
            }
        }

    }
}