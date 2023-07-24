using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass.ERP.ViewModel
{
    public class PageViewModel
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public string PageRoutePath { get; set; }
        public int? SerialNo { get; set; }
        public string ModuleName { get; set; }
    }
}
