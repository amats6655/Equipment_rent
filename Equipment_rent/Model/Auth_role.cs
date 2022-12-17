using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Equipment_rent.Model
{
    public class Auth_role
    {
        [Key] public Guid Id { get; set; }
        public string Role { get; set; }
        public List<Auth_user> auth_User { get; set; }
    }
}
