using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace GroceryListSystem.ViewModels
{
    public class ProductItem
    {
        public int ProductID { get; set; }
        [Required (ErrorMessage ="Description is required")]
        [StringLength(100,ErrorMessage ="Description is limited to 100 characters")]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        [Required(ErrorMessage = "Unit Size is required")]
        [StringLength(20, ErrorMessage = "Unit Size is limited to 20 characters")]
        public string UnitSize { get; set; }
        public int CategoryID { get; set; }
        public bool Taxable { get; set; }
        public byte[] Photo { get; set; }

    }
}
