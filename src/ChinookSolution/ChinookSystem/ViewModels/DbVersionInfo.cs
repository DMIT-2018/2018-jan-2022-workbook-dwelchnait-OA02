using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.ViewModels
{
    public  class DbVersionInfo
    {
        //the view that is used by the "outside world"
        //access must match the method where the class is used (typically public)
        //propose: use to simply carry data
        //         created data fields as auto-implemented properties
        //         consists of the "raw" data of the query
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
