﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class UserModel

    {
        public string URL { get; set; }
        public string UserName { get; set; }
        public string? Photo { get; set; }
        public string? Bio { get; set; }
        public string? Email { get; set; }
    }
}