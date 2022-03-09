
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using GroceryListSystem.DAL;
using GroceryListSystem.Entities;
using GroceryListSystem.ViewModels;
#endregion

namespace GroceryListSystem.BLL
{
    public class CategoryServices
    {
        #region Constructor and Context Dependency
        private readonly GroceryListContext _context;

        internal CategoryServices(GroceryListContext context)
        {
            _context = context;
        }
        #endregion

        #region Services Query
        public List<SelectionList> Category_GetList()
        {
            IEnumerable<SelectionList> info = _context.Categories
                                .OrderBy(x => x.Description)
                                .Select(x => new SelectionList
                                {
                                    ValueId = x.CategoryID,
                                    DisplayText = x.Description
                                });
            return info.ToList();
        }
        #endregion
    }
}
