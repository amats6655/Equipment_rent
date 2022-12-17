using Equipment_rent.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

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
                var result = db.Orders.OrderBy(o => o.IsReturned).ToList();

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
                bool checkIsExist = db.Equipments.Any(el => el.Model == model);
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
        public static string CreateOrder(User user, Equipment equipment, int amount, DateTime dateIssue, DateTime dateReturn, Guid WhoGive)
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
                    IsReturned = false,
                    WhoGive = WhoGive
                };
                db.Orders.Add(newOrder);
                db.SaveChanges();

            }
            return result;
        }
        // Add Staff
        public static Auth_user CreateStaff(Guid userId, string username, string firstname, string lastname, string email, Guid role_id)
        {
            Auth_user staff = new Auth_user();
            using (ApplicationContext db = new ApplicationContext())
            {
                // Проверяем на наличие такого пользователя
                bool checkIsExist = db.Auth_user.Any(el => el.Username == username && el.Email == email);
                if (!checkIsExist)
                {
                    Auth_user newStaff = new Auth_user {Id = userId, Username = username, FirstName = firstname, LastName = lastname, Email = email, Role_Id = role_id, Auth_roleId = role_id};
                    db.Auth_user.Add(newStaff);
                    db.SaveChanges();
                    staff = newStaff;
                }
                return staff;
            }
        }

        // Edit User
        public static string EditUser(User oldUser, string newName, string newPhone, bool isDebt)
        {
            string result;
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.FirstOrDefault(u => u.UserId == oldUser.UserId);
                user.Name = newName;
                user.Phone = newPhone;
                user.Debt = isDebt;
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
        public static string EditOrder(Order oldOrder, User newUser, Equipment newEquipment, int newAmount, DateTime newDateIssue, DateTime newDateReturn, bool newIsReturned, Guid WhoTake)
        {
            string result;
            using (ApplicationContext db = new ApplicationContext())
            {
                Order order = db.Orders.FirstOrDefault(o => o.OrderId == oldOrder.OrderId);
                order.UserId = newUser.UserId;
                order.EquipmentId = newEquipment.EquipmentId;
                order.Amount = newAmount;
                order.DateIssue = newDateIssue;
                order.DateReturn = newDateReturn;
                order.IsReturned = newIsReturned;
                if(WhoTake != Guid.Empty)
                {
                    order.WhoTake = WhoTake;
                }
                
                db.SaveChanges();
                result = "Сделано!";
            }
            return result;
        }





        // Edit Staff
        public static string EditStaff(Auth_user oldStaff, string firstname, string lastname, string email, Guid role_id)
        {
            string result;
            using (ApplicationContext db = new ApplicationContext())
            {
                Auth_user staff = db.Auth_user.FirstOrDefault(s => s.Id == oldStaff.Id);
                staff.FirstName = firstname;
                staff.LastName = lastname;
                staff.Email = email;
                staff.Role_Id = role_id;
                staff.Auth_roleId = role_id;
                db.SaveChanges();
                result = "Сделано!";
                return result;
            }
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
        // Delete Staff
        public static string DeleteStaff(Auth_user staff)
        {
            string result;
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Auth_user.Remove(staff);
                db.SaveChanges();
                result = $"Сделано! Пользователь {staff.Username} уволен";
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


        // Get All Equipments by Id Type
        public static List<Equipment> GetAllEquipmentsByIdType(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Equipment> equipments = (from equipment in GetAllEquipments() where equipment.TypeId == id select equipment).ToList();
                return equipments;
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

        // Get all orders by status
        public static List<Order> GetAllOrdersByStatus()
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                List<Order> orders = (from order in GetAllOrders() where order.IsReturned == false select order).ToList();
                return orders;
            }
        }



        // Get Preview Users
        public static List<User> GetPreviousPageUsers(int pageIndex, int count)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (pageIndex > 1)
                {
                    pageIndex -= 1;
                    UsersVM.pageIndex = pageIndex;
                    if (pageIndex == 1)
                    {
                        var result = db.Users.Take(count).ToList();
                        UsersVM.count = result.Count();
                        return result;
                    }
                    else
                    {
                        var result = db.Users.Skip((pageIndex * count) - count).Take(count).ToList();
                        UsersVM.count = Math.Min(pageIndex * count, GetAllUsers().Count);
                        return result;
                    }

                }
                else
                {
                    return db.Users.Take(count).ToList();
                }
                
            }
        }        
        // Get Preview Equipments
        public static List<Equipment> GetPreviousPageEquipments(int pageIndex, int count)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (pageIndex > 1)
                {
                    pageIndex -= 1;
                    EquipmentsVM.pageIndex = pageIndex;
                    if(pageIndex == 1)
                    {
                        var result = db.Equipments.Take(count).ToList();
                        EquipmentsVM.count = result.Count();
                        return result;
                    }
                    else
                    {
                        var result = db.Equipments.Skip((pageIndex * count) - count).Take(count).ToList();
                        EquipmentsVM.count = Math.Min(pageIndex * count, GetAllUsers().Count);
                        return result;
                    }
                }
                else
                {
                    return db.Equipments.Take(count).ToList();
                }
            }
        }        
        // Get Preview Orders
        public static List<Order> GetPreviousPageOrders(int pageIndex,int count)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (pageIndex > 1)
                {
                    pageIndex -= 1;
                    OrdersVM.pageIndex = pageIndex;
                    if (pageIndex == 1)
                    {
                        var result = db.Orders.Take(count).ToList();
                        OrdersVM.count = result.Count();
                        return result;
                    }
                    else
                    {
                        var result = db.Orders.Skip((pageIndex * count)-count).Take(count).ToList();
                        OrdersVM.count = Math.Min(pageIndex * count, GetAllOrders().Count);
                        return result;
                    }
                }
                else
                {
                    return db.Orders.Take(count).ToList();
                }
                
            }
        }
        // Get Preview Staff
        public static List<Auth_user> GetPreviousPageStaff(int pageIndex, int count)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (pageIndex > 1)
                {
                    pageIndex -= 1;
                    StaffVM.pageIndex = pageIndex;
                    if (pageIndex == 1)
                    {
                        var result = db.Auth_user.Take(count).ToList();
                        StaffVM.count = result.Count();
                        return result;
                    }
                    else
                    {
                        var result = db.Auth_user.Skip((pageIndex * count) - count).Take(count).ToList();
                        StaffVM.count = Math.Min(pageIndex * count, GetAllAuthUsers().Count);
                        return result;
                    }

                }
                else
                {
                    return db.Auth_user.Take(count).ToList();
                }

            }
        }



        // Get Next Users
        public static List<User> GetNextPageUsers(int pageIndex, int count)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                
                if(db.Users.Skip(pageIndex * count).Take(count).Count() == 0)
                {
                    var result = db.Users.Skip((pageIndex * count) - count).Take(count).ToList();
                    UsersVM.count = (pageIndex * count) + db.Users.Skip(pageIndex * count).Take(count).Count();
                    return result;
                }
                else
                {
                    var result = db.Users.Skip(pageIndex * count).Take(count).ToList();
                    UsersVM.count = (pageIndex * count) + db.Users.Skip(pageIndex * count).Take(count).Count();
                    UsersVM.pageIndex = pageIndex + 1;
                    return result;
                }
                
            }
        }        
        // Get Next Equipments
        public static List<Equipment> GetNextPageEquipments(int pageIndex, int count)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                
                if (db.Equipments.Skip(pageIndex * count).Take(count).Count() == 0)
                {
                    var result = db.Equipments.Skip((pageIndex * count) - count).Take(count).ToList();
                    EquipmentsVM.count = (pageIndex * count) + db.Equipments.Skip(pageIndex * count).Take(count).Count();
                    return result;
                }
                else
                {
                    var result = db.Equipments.Skip(pageIndex * count).Take(count).ToList();
                    EquipmentsVM.count = (pageIndex * count) + db.Equipments.Skip(pageIndex * count).Take(count).Count();
                    EquipmentsVM.pageIndex = pageIndex + 1;
                    return result;
                }
                
            }
        }        
        // Get next Orders
        public static List<Order> GetNextPageOrders(int pageIndex,int count)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (db.Orders.Skip(pageIndex * count).Take(count).Count() == 0)
                {
                    var result = db.Orders.Skip((pageIndex * count) - count).Take(count).ToList();
                    OrdersVM.count = (pageIndex * count) + db.Orders.Skip(pageIndex * count).Take(count).Count();
                    return result;
                }
                else
                {
                    var result = db.Orders.Skip(pageIndex * count).Take(count).ToList();
                    OrdersVM.count = (pageIndex * count) + db.Orders.Skip(pageIndex * count).Take(count).Count();
                    OrdersVM.pageIndex = pageIndex + 1;
                    return result;
                }
                
            }
        }
        // Get Next Staff
        public static List<Auth_user> GetNextPageStaff(int pageIndex, int count)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                if (db.Users.Skip(pageIndex * count).Take(count).Count() == 0)
                {
                    var result = db.Auth_user.Skip((pageIndex * count) - count).Take(count).ToList();
                    StaffVM.count = (pageIndex * count) + db.Auth_user.Skip(pageIndex * count).Take(count).Count();
                    return result;
                }
                else
                {
                    var result = db.Auth_user.Skip(pageIndex * count).Take(count).ToList();
                    StaffVM.count = (pageIndex * count) + db.Auth_user.Skip(pageIndex * count).Take(count).Count();
                    StaffVM.pageIndex = pageIndex + 1;
                    return result;
                }

            }
        }


        //Get First Users
        public static List<User> GetFirstUsers(int count)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Users.Take(count).ToList();
                return result;
            }
        }

        //Get First Equipments
        public static List<Equipment> GetFirstEquipments(int count)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Equipments.Take(count).ToList();
                return result;
            }
        }

        //Get First Orders
        public static List<Order> GetFirstOrders(int count)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Orders.Take(count).OrderBy(o => o.IsReturned).ToList();

                return result;
            }
        }

        //Get First Staff
        public static List<Auth_user> GetFirstStaff(int count)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Auth_user.Take(count).ToList();
                return result;
            }
        }



        // Get All Auth_users
        public static List<Auth_user> GetAllAuthUsers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Auth_user.ToList();
                return result;
            }
        }

        // Get All Roles
        public static List<Auth_role> GetAllAuthRoles()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Auth_role.ToList();
                return result;
            }
        }
       
        // Get User By Id
        public static Auth_user GetAuthUserById(Guid id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Console.WriteLine("Список пользователей:");
                var result = db.Auth_user.FirstOrDefault(p => p.Id.Equals(id));
                return result;
            }
        }

        // Get Role By Id
        public static Auth_role GetRoleById(Guid roleId)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Auth_role.FirstOrDefault(p => p.Id.Equals(roleId));
                return result;
            }
        }

        // Edit Auth_User
        public static string EditAuthUser(
                Auth_user oldUser,
                Guid id,
                string firstName,
                string lastName,
                string email,
                Guid newRole)
        {
            string result;
            using (ApplicationContext db = new ApplicationContext())
            {
                Auth_user user = db.Auth_user.FirstOrDefault(u => u.Id == oldUser.Id);
                user.Id = id;
                user.FirstName = firstName;
                user.LastName = lastName;
                user.Email = email;
                user.Role_Id = newRole;
                db.SaveChanges();
                result = "Сделано!";
                return result;
            }
        }
    }
}
