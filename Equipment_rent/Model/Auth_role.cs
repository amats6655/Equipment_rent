﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;

namespace Equipment_rent.Model
{
    public class Auth_role
    {
        [Key] public string Id { get; set; }
        public string Role { get; set; }
    }
}
