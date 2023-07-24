﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass.ERP.ViewModel
{
    public class UnitViewModel
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public string? UnitDescription { get; set; }
        public decimal UnitFactor { get; set; }
        public int? IsBox { get; set; }
        public int CompId { get; set; }
        public int? Status { get; set; }
    }
}
