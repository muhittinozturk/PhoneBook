﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Report
    {
        public string Id { get; set; }
        public string? Explain { get; set; }
        public ICollection<ReportDetail>? ReportDetails { get; set; }
    }
}
