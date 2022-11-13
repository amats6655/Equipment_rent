using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Equipment_rent.Model
{
    public class Equipment
    {
        [Key] public int EquipmentId { get; set; }
        public int TypeId { get; set; }
        public virtual Type Type { get; set; }
        public string Model { get; set; }
        public int Amount { get; set; }
        public int Balance { get; set; }
        public List<Order>? Orders { get; set; }

        [NotMapped] public Type EquipType
        {
            get
            {
                return DataWorker.GetTypeById(TypeId);
            }
        }
        [NotMapped] public List<Order> EquipmentOrders
        {
            get
            {
                return DataWorker.GetAllOrdersByEquipmetnId(EquipmentId);
            }
        }
    }
}
