﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Equipment_rent.Model
{
    public class User
    {
        [Key] public int UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool Debt { get; set; }
        public List<Order>? Orders { get; set; }

        [NotMapped]public string? BgColor { get; set; }
        [NotMapped]public char? Character { get; set; }
    }
}

