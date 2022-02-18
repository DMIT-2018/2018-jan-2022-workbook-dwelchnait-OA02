#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.ViewModels
{
    public class SelectionList
    {
        //common class used to hold 2 values for use in a select list scenarion 
        //  such as a drop down list
        public int ValueId { get; set; }
        public string DisplayText { get; set; }
    }
}
