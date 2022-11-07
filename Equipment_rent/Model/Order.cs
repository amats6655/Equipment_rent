using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equipment_rent.Model
{
    public class Order
    {
        [Key] public int OrderId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public Equipment Equipment { get; set; }
        public int EquipmentId { get; set; }
        public DateTime DateIssue { get; set; }
        public DateTime? DateReturn { get; set; }
        public bool IsReturned { get; set; }
        public int Amount { get; set; }
    }
}
