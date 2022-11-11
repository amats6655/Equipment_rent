using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Equipment_rent.View;
using System.Data;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Equipment_rent.Model
{
    public static class DataWorker
    {
        // Get All Users
        public static List<User> GetAllUsers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Users.ToList();

                return result;
            }
        }

        // Get All Equipments
        public static List<Equipment> GetAllEquipments()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Equipments.ToList();

                return result;
            }
        }

        //Get All Orders
        public static List<Order> GetAllOrders()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Orders.ToList();

                return result;
            }
        }

        //Get All Types
        public static List<Type> GetAllTypes()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Types.ToList();

                return result;
            }
        }

        // Add User
        public static User CreateUser(string name, string phone, bool debt)
        {
            User user = new User();
            using (ApplicationContext db = new ApplicationContext())
            {
                // Проверяем на наличие такого пользователя
                bool checkIsExist = db.Users.Any(el => el.Phone == phone && el.Name == name);
                if (!checkIsExist)
                {
                    User newUser = new User { Name = name, Phone = phone, Debt = debt };
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    user = newUser;
                }
                return user;
            }
        }

        // Add Equipment
        public static string CreateEquip(Type type, string model, int amount)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                // Проверяем на наличие такого оборудование
                bool checkIsExist = db.Equipments.Any(el => el.Model == model && el.Type == type);
                if (!checkIsExist)
                {
                    Equipment newEquipment = new Equipment
                    {
                        TypeId = type.TypeId,
                        Model = model,
                        Amount = amount,
                        Balance = amount
                    };
                    db.Equipments.Add(newEquipment);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }

        // Add Order
        public static string CreateOrder(User user, Equipment equipment, int amount, DateTime dateIssue, DateTime dateReturn)
        {
            string result = "Сделано!";
            using (ApplicationContext db = new ApplicationContext())
            {
                Order newOrder = new Order
                {
                    UserId = user.UserId,
                    EquipmentId = equipment.EquipmentId,
                    Amount = amount,
                    DateIssue = dateIssue,
                    DateReturn = dateReturn,
                    IsReturned = false
                };
                db.Orders.Add(newOrder);
                db.SaveChanges();

            }
            return result;
        }

        // Edit User
        public static string EditUser(User oldUser, string newName, string newPhone)
        {
            string result;
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.FirstOrDefault(u => u.UserId == oldUser.UserId);
                user.Name = newName;
                user.Phone = newPhone;
                db.SaveChanges();
                result = "Сделано!";
                return result;
            }
        }

        // Edit Equipment
        public static string EditEquipment(Equipment oldEquipment, Type newType, string newModel, int newAmount, int newBalance)
        {
            string result;
            using (ApplicationContext db = new ApplicationContext())
            {
                Equipment equipment = db.Equipments.FirstOrDefault(e => e.EquipmentId == oldEquipment.EquipmentId);
                equipment.TypeId = newType.TypeId;
                equipment.Model = newModel;
                equipment.Amount = newAmount;
                equipment.Balance = newBalance;
                db.SaveChanges();
                result = "Сделано!";
                return result;
            }
        }

        // Edit Order
        public static string EditOrder(Order oldOrder, User newUser, Equipment newEquipment, DateTime newDateIssue, DateTime newDateReturn, bool newIsReturned)
        {
            string result;
            using (ApplicationContext db = new ApplicationContext())
            {
                Order order = db.Orders.FirstOrDefault(o => o.OrderId == oldOrder.OrderId);
                order.UserId = newUser.UserId;
                order.EquipmentId = newEquipment.EquipmentId;
                order.DateIssue = newDateIssue;
                order.DateReturn = newDateReturn;
                order.IsReturned = newIsReturned;
                db.SaveChanges();
                result = "Сделано!";
            }
            return result;
        }

        // Delete User
        public static string DeleteUser(User user)
        {
            string result;
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Users.Remove(user);
                db.SaveChanges();
                result = $"Сделано! Пользователь {user.Name} уволен";
            }
            return result;
        }

        // Delete Equipment
        public static string DeleteEquipment(Equipment equipment)
        {
            string result;
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Equipments.Remove(equipment);
                db.SaveChanges();
                result = $"Сделано! Оборудование {equipment.Model} удалено";
            }
            return result;
        }

        // Delete Order
        public static string DeleteOrder(Order order)
        {
            string result;
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Orders.Remove(order);
                db.SaveChanges();
                result = $"Сделано! Заказ #{order.OrderId} удален";
            }
            return result;
        }

        // Get Type by Id
        public static Type GetTypeById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Type type = db.Types.FirstOrDefault(p => p.TypeId == id);
                return type;
            }
        }

        // Get Equipment by Id
        public static Equipment GetEquipmentById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Equipment equip = db.Equipments.FirstOrDefault(p => p.EquipmentId == id);
                return equip;
            }
        }

        // Get User by Id
        public static User GetUserById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.FirstOrDefault(p => p.UserId == id);
                return user;
            }
        }

        // Get All Orders by Id User
        public static List<Order> GetAllOrdersByUserId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Order> orders = (from order in GetAllOrders() where order.UserId == id && order.IsReturned == false select order).ToList();
                return orders;
            }
        }

        // Get All Orders by Id Equipment
        public static List<Order> GetAllOrdersByEquipmetnId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Order> orders = (from order in GetAllOrders() where order.EquipmentId == id && order.IsReturned == false select order).ToList();
                return orders;
            }
        }
    }
}
