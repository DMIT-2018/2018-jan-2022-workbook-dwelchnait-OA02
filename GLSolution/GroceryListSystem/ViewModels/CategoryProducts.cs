using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryListSystem.ViewModels
{
    public class CategoryProducts
    {
        public int ProductID { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal SalePrice { get { return Price - Discount; } }
        public string UnitSize { get; set; }
        public bool Taxable { get; set; }
    }
}
