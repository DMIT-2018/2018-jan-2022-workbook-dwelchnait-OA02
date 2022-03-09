#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using GroceryListSystem.DAL;
using GroceryListSystem.Entities;
using GroceryListSystem.ViewModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;
#endregion

namespace GroceryListSystem.BLL
{
    public class ProductServices
    {
        #region Constructor and Context Dependency
        private readonly GroceryListContext _context;

        internal ProductServices(GroceryListContext context)
        {
            _context = context;
        }
        #endregion

        #region Services Query
        public List<CategoryProduct> Products_GetByCategory(int categoryid,
                                                            int pagenumber,
                                                            int pagesize,
                                                            out int totalcount)
        {
            IEnumerable<CategoryProduct> info = _context.Products
                                .Where(x => x.CategoryID == categoryid)
                                .OrderBy(x => x.Description)
                                .Select(x => new CategoryProduct
                                {
                                    ProductID = x.ProductID,
                                    Description = x.Description,
                                    Price = x.Price,
                                    Discount = x.Discount,
                                    UnitSize = x.UnitSize,
                                    Taxable = x.Taxable
                                });
            totalcount = info.Count();
            int skipRows = (pagenumber - 1) * pagesize;
            return info.Skip(skipRows).Take(pagesize).ToList();
        }

        public ProductItem Products_GetProduct(int productid)
        {
            ProductItem info = _context.Products
                            .Where(x => x.ProductID == productid)
                            .Select(x => new ProductItem
                            {
                                ProductID = x.ProductID,
                                Description= x.Description,
                                Price= x.Price,
                                Discount= x.Discount,
                                UnitSize= x.UnitSize,
                                Taxable= x.Taxable
                            }).FirstOrDefault();
            return info;
        }
        #endregion

        #region Services Add, Update, Delete
        public int Products_AddProduct(ProductItem item)
        {
            Product newItem = new Product
            {
                Description = item.Description,
                Price =item.Price,
                Discount = item.Discount / 100.0m,
                UnitSize = item.UnitSize,
                CategoryID = item.CategoryID,
                Taxable = item.Taxable,
                Photo = null
            };
            //stage add in local memory
            _context.Add(newItem);
            //do any validation within the entity (validation anotation)
            //send stage request to the database for processing (transaction)
            _context.SaveChanges();
            return newItem.ProductID;
        }
        public int Products_UpdateProduct(ProductItem item)
        {
            Product exist = _context.Products
                            .Where(x => x.ProductID == item.ProductID)
                            .FirstOrDefault();
            if (exist == null)
            {
                throw new Exception("Product does not exist on file");
            }

            exist.Description = item.Description;
            exist.Price =item.Price;
            exist.Discount = item.Discount / 100.0m;
            exist.UnitSize = item.UnitSize;
            exist.CategoryID = item.CategoryID;
            exist.Taxable = item.Taxable;
            exist.Photo = null;

            EntityEntry<Product> updating = _context.Entry(exist);
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return _context.SaveChanges();

        }

        public int Products_DeleteProduct(ProductItem item)
        {
            Product exist = _context.Products
                            .Where(x => x.ProductID == item.ProductID)
                            .FirstOrDefault();
            if (exist == null)
            {
                throw new Exception("Product does not exist on file");
            }

            EntityEntry<Product> deleting = _context.Entry(exist);
            deleting.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            return _context.SaveChanges();
        }
        #endregion
    }
}
