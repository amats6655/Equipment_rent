﻿using System.ComponentModel.DataAnnotations;

namespace Equipment_rent.Model
{
    public class Type
    {
        [Key] public int TypeId { get; set; }
        public string Name { get; set; }
    }
}
