using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Equipment_rent.Model
{
    public class Auth_user
    {
        [Key] public Guid Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid Role_Id { get; set; }
        [NotMapped] public Auth_role Role { get { return DataWorker.GetRoleById(Role_Id); } }
    }
}
