﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class SearchResult
    {
        public string TitleId { get; set; }
        public string? TitleName { get; set; }
        public string? Plot { get; set; }
        public string? Character { get; set; }
        public string? ActorNames { get; set; }

    }
}
