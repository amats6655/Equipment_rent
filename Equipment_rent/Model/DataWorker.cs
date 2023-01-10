using Equipment_rent.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Equipment_rent.Model
{
    public static class DataWorker
    {
        // Get All Users
        public static List<User> GetAllUsers()
        {
            using var db = new ApplicationContext();
            var result = db.Users.OrderBy(o => o.Name).ToList();

            return result;
        }

        // Get All Equipments
        public static List<Equipment> GetAllEquipments()
        {
            using var db = new ApplicationContext();
            var result = db.Equipments.OrderBy(o => o.TypeId).ToList();

            return result;
        }

        //Get All Orders
        public static List<Order> GetAllOrders()
        {
            using var db = new ApplicationContext();
            var result = db.Orders.OrderBy(o => o.IsReturned).ToList();

            return result;
        }

        //Get All Types
        public static List<Type> GetAllTypes()
        {
            using var db = new ApplicationContext();
            var result = db.Types.ToList();

            return result;
        }


        // Get All Auth_users
        public static List<Auth_user> GetAllAuthUsers()
        {
            using var db = new ApplicationContext();
            var result = db.Auth_user.OrderBy(o => o.Username).ToList();

            return result;
        }

        // Get All Roles
        public static List<Auth_role> GetAllAuthRoles()
        {
            using var db = new ApplicationContext();
            var result = db.Auth_role.ToList();

            return result;
        }

        // Add User
        public static User CreateUser(string name, string phone, bool debt)
        {
            var user = new User();
            using var db = new ApplicationContext();
            var checkIsExist = db.Users.Any(el => el.Phone == phone && el.Name == name);
            if (!checkIsExist)
            {
                var newUser = new User { Name = name, Phone = phone, Debt = debt };
                db.Users.Add(newUser);
                db.SaveChanges();
                user = newUser;
            }
            return user;
        }


        // Add Equipment
        public static void CreateEquip(Type type, string model, int amount)
        {
            using var db = new ApplicationContext();
            var checkIsExist = db.Equipments.Any(el => el.Model == model);
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
            }
        }

        // Add Order
        public static void CreateOrder(User user, Equipment equipment, int amount, DateTime dateIssue, DateTime dateReturn, Guid whoGive)
        {
            using var db = new ApplicationContext();
            var newOrder = new Order
            {
                UserId = user.UserId,
                EquipmentId = equipment.EquipmentId,
                Amount = amount,
                DateIssue = dateIssue,
                DateReturn = dateReturn,
                IsReturned = false,
                WhoGive = whoGive
            };
            db.Orders.Add(newOrder);
            db.SaveChanges();
        }
        // Add Staff
        public static Auth_user CreateStaff(Guid userId, string username, string firstname, string lastname, string email, Guid roleId)
        {
            var staff = new Auth_user();
            using var db = new ApplicationContext();
            var checkIsExist = db.Auth_user.Any(el => el.Username == username && el.Email == email);
            if (!checkIsExist)
            {
                Auth_user newStaff = new Auth_user { Id = userId, Username = username, FirstName = firstname, LastName = lastname, Email = email, Role_Id = roleId, Auth_roleId = roleId };
                db.Auth_user.Add(newStaff);
                db.SaveChanges();
                staff = newStaff;
            }
            return staff;
        }

        // Edit User
        public static void EditUser(User oldUser, string newName, string newPhone, bool isDebt)
        {
            using var db = new ApplicationContext();
            var user = db.Users.FirstOrDefault(u => u.UserId == oldUser.UserId);
            if (user != null)
            {
                user.Name = newName;
                user.Phone = newPhone;
                user.Debt = isDebt;
            }

            db.SaveChanges();
        }

        // Edit Equipment
        public static void EditEquipment(Equipment oldEquipment, Type newType, string newModel, int newAmount, int newBalance)
        {
            using var db = new ApplicationContext();
            var equipment = db.Equipments.FirstOrDefault(e => e.EquipmentId == oldEquipment.EquipmentId);
            if (equipment != null)
            {
                equipment.TypeId = newType.TypeId;
                equipment.Model = newModel;
                equipment.Amount = newAmount;
                equipment.Balance = newBalance;
            }

            db.SaveChanges();
        }

        // Edit Order
        public static void EditOrder(Order oldOrder, User newUser, Equipment newEquipment, int newAmount, DateTime newDateIssue, DateTime newDateReturn, bool newIsReturned, Guid whoTake)
        {
            using var db = new ApplicationContext();
            var order = db.Orders.FirstOrDefault(o => o.OrderId == oldOrder.OrderId);
            if (order != null)
            {
                order.UserId = newUser.UserId;
                order.EquipmentId = newEquipment.EquipmentId;
                order.Amount = newAmount;
                order.DateIssue = newDateIssue;
                order.DateReturn = newDateReturn;
                order.IsReturned = newIsReturned;
                if (whoTake != Guid.Empty)
                {
                    order.WhoTake = whoTake;
                }
            }

            db.SaveChanges();
        }





        // Edit Staff
        public static void EditStaff(Auth_user oldStaff, string firstname, string lastname, string email, Guid roleId)
        {
            using var db = new ApplicationContext();
            var staff = db.Auth_user.FirstOrDefault(s => s.Id == oldStaff.Id);
            if (staff != null)
            {
                staff.FirstName = firstname;
                staff.LastName = lastname;
                staff.Email = email;
                staff.Role_Id = roleId;
                staff.Auth_roleId = roleId;
            }

            db.SaveChanges();
        }

        // Delete User
        public static void DeleteUser(User user)
        {
            using var db = new ApplicationContext();
            db.Users.Remove(user);
            db.SaveChanges();
        }

        // Delete Equipment
        public static void DeleteEquipment(Equipment equipment)
        {
            using var db = new ApplicationContext();
            db.Equipments.Remove(equipment);
            db.SaveChanges();
        }

        // Delete Order
        public static void DeleteOrder(Order order)
        {
            using var db = new ApplicationContext();
            db.Orders.Remove(order);
            db.SaveChanges();
        }
        // Delete Staff
        public static void DeleteStaff(Auth_user staff)
        {
            using var db = new ApplicationContext();
            db.Auth_user.Remove(staff);
            db.SaveChanges();
        }

        // Get Type by Id
        public static Type GetTypeById(int id)
        {
            using var db = new ApplicationContext();
            var type = db.Types.FirstOrDefault(p => p.TypeId == id);
            return type ?? throw new InvalidOperationException();
        }

        // Get Equipment by Id
        public static Equipment GetEquipmentById(int id)
        {
            using var db = new ApplicationContext();
            var equip = db.Equipments.FirstOrDefault(p => p.EquipmentId == id);
            return equip ?? throw new InvalidOperationException();
        }

        // Get User by Id
        public static User GetUserById(int id)
        {
            using var db = new ApplicationContext();
            var user = db.Users.FirstOrDefault(p => p.UserId == id);
            return user ?? throw new InvalidOperationException();
        }

        // Get User By Name
        public static User GetUserByName(string name)
        {
            using var db = new ApplicationContext();
            var user = db.Users.FirstOrDefault(p => p.Name == name);
            return user ?? throw new InvalidOperationException();
        }


        // Get All Equipments by Id Type
        public static List<Equipment> GetAllEquipmentsByIdType(int id)
        {
            var equipments = (from equipment in GetAllEquipments() where equipment.TypeId == id select equipment).ToList();
            return equipments;
        }

        // Get All Orders by Id User
        public static List<Order> GetAllOrdersByUserId(int id)
        {
            var orders = (from order in GetAllOrders() where order.UserId == id && order.IsReturned == false select order).ToList();
            return orders;
        }

        // Get All Orders by Id Equipment
        public static List<Order> GetAllOrdersByEquipmentId(int id)
        {
            var orders = (from order in GetAllOrders() where order.EquipmentId == id && order.IsReturned == false select order).ToList();
            return orders;
        }

        // Get all orders by status
        public static List<Order> GetAllOrdersByStatus()
        {
            List<Order> orders = (from order in GetAllOrders() where order.IsReturned == false select order).ToList();
            return orders;
        }



        // Get Preview Users
        public static List<User> GetPreviousPageUsers(int pageIndex, int count)
        {
            if (pageIndex > 1)
            {
                pageIndex -= 1;
                UsersVM.pageIndex = pageIndex;
                if (pageIndex == 1)
                {
                    var result = GetAllUsers().Take(count).ToList();
                    UsersVM.count = result.Count;
                    return result;
                }
                else
                {
                    var result = GetAllUsers().Skip((pageIndex * count) - count).Take(count).ToList();
                    UsersVM.count = Math.Min(pageIndex * count, GetAllUsers().Count);
                    return result;
                }

            }
            else
            {
                return GetAllUsers().Take(count).ToList();
            }
        }
        // Get Preview Equipments
        public static List<Equipment> GetPreviousPageEquipments(int pageIndex, int count)
        {
            if (pageIndex > 1)
            {
                pageIndex -= 1;
                EquipmentsVM.pageIndex = pageIndex;
                if (pageIndex == 1)
                {
                    var result = GetAllEquipments().Take(count).ToList();
                    EquipmentsVM.count = result.Count;
                    return result;
                }
                else
                {
                    var result = GetAllEquipments().Skip((pageIndex * count) - count).Take(count).ToList();
                    EquipmentsVM.count = Math.Min(pageIndex * count, GetAllUsers().Count);
                    return result;
                }
            }
            else
            {
                return GetAllEquipments().Take(count).ToList();
            }
        }
        // Get Preview Orders
        public static List<Order> GetPreviousPageOrders(int pageIndex, int count)
        {
            if (pageIndex > 1)
            {
                pageIndex -= 1;
                OrdersVM.PageIndex = pageIndex;
                if (pageIndex == 1)
                {
                    var result = GetAllOrders().Take(count).ToList();
                    OrdersVM.Count = result.Count;
                    return result;
                }
                else
                {
                    var result = GetAllOrders().Skip((pageIndex * count) - count).Take(count).ToList();
                    OrdersVM.Count = Math.Min(pageIndex * count, GetAllOrders().Count);
                    return result;
                }
            }
            else
            {
                return GetAllOrders().Take(count).ToList();
            }
        }
        // Get Preview Staff
        public static List<Auth_user> GetPreviousPageStaff(int pageIndex, int count)
        {
            if (pageIndex > 1)
            {
                pageIndex -= 1;
                StaffVM.pageIndex = pageIndex;
                if (pageIndex == 1)
                {
                    var result = GetAllAuthUsers().Take(count).ToList();
                    StaffVM.count = result.Count;
                    return result;
                }
                else
                {
                    var result = GetAllAuthUsers().Skip((pageIndex * count) - count).Take(count).ToList();
                    StaffVM.count = Math.Min(pageIndex * count, GetAllAuthUsers().Count);
                    return result;
                }

            }
            else
            {
                return GetAllAuthUsers().Take(count).ToList();
            }
        }



        // Get Next Users
        public static List<User> GetNextPageUsers(int pageIndex, int count)
        {
            if (!GetAllUsers().Skip(pageIndex * count).Take(count).Any())
            {
                var result = GetAllUsers().Skip((pageIndex * count) - count).Take(count).ToList();
                UsersVM.count = (pageIndex * count) + GetAllUsers().Skip(pageIndex * count).Take(count).Count();
                return result;
            }
            else
            {
                var result = GetAllUsers().Skip(pageIndex * count).Take(count).ToList();
                UsersVM.count = (pageIndex * count) + GetAllUsers().Skip(pageIndex * count).Take(count).Count();
                UsersVM.pageIndex = pageIndex + 1;
                return result;
            }
        }
        // Get Next Equipments
        public static List<Equipment> GetNextPageEquipments(int pageIndex, int count)
        {
            if (!GetAllEquipments().Skip(pageIndex * count).Take(count).Any())
            {
                var result = GetAllEquipments().Skip((pageIndex * count) - count).Take(count).ToList();
                EquipmentsVM.count = (pageIndex * count) + GetAllEquipments().Skip(pageIndex * count).Take(count).Count();
                return result;
            }
            else
            {
                var result = GetAllEquipments().Skip(pageIndex * count).Take(count).ToList();
                EquipmentsVM.count = (pageIndex * count) + GetAllEquipments().Skip(pageIndex * count).Take(count).Count();
                EquipmentsVM.pageIndex = pageIndex + 1;
                return result;
            }
        }
        // Get next Orders
        public static List<Order> GetNextPageOrders(int pageIndex, int count)
        {
            if (!GetAllOrders().Skip(pageIndex * count).Take(count).Any())
            {
                var result = GetAllOrders().Skip((pageIndex * count) - count).Take(count).ToList();
                OrdersVM.Count = (pageIndex * count) + GetAllOrders().Skip(pageIndex * count).Take(count).Count();
                return result;
            }
            else
            {
                var result = GetAllOrders().Skip(pageIndex * count).Take(count).ToList();
                OrdersVM.Count = (pageIndex * count) + GetAllOrders().Skip(pageIndex * count).Take(count).Count();
                OrdersVM.PageIndex = pageIndex + 1;
                return result;
            }
        }
        // Get Next Staff
        public static List<Auth_user> GetNextPageStaff(int pageIndex, int count)
        {
            if (!GetAllAuthUsers().Skip(pageIndex * count).Take(count).Any())
            {
                var result = GetAllAuthUsers().Skip((pageIndex * count) - count).Take(count).ToList();
                StaffVM.count = (pageIndex * count) + GetAllAuthUsers().Skip(pageIndex * count).Take(count).Count();
                return result;
            }
            else
            {
                var result = GetAllAuthUsers().Skip(pageIndex * count).Take(count).ToList();
                StaffVM.count = (pageIndex * count) + GetAllAuthUsers().Skip(pageIndex * count).Take(count).Count();
                StaffVM.pageIndex = pageIndex + 1;
                return result;
            }
        }


        //Get First Users
        public static List<User> GetFirstUsers(int count)
        {
            var result = GetAllUsers().Take(count).ToList();
            return result;
        }

        //Get First Equipments
        public static List<Equipment> GetFirstEquipments(int count)
        {
            var result = GetAllEquipments().Take(count).ToList();
            return result;
        }

        //Get First Orders
        public static List<Order> GetFirstOrders(int count)
        {
            var result = GetAllOrders().Take(count).ToList();
            return result;
        }

        //Get First Staff
        public static List<Auth_user> GetFirstStaff(int count)
        {
            var result = GetAllAuthUsers().Take(count).ToList();
            return result;
        }





        // Get User By Id
        public static Auth_user GetAuthUserById(Guid id)
        {
            using var db = new ApplicationContext();
            var result = db.Auth_user.FirstOrDefault(p => p.Id.Equals(id));
            return result ?? throw new InvalidOperationException();
        }

        // Get Role By Id
        public static Auth_role GetRoleById(Guid roleId)
        {
            using var db = new ApplicationContext();
            var result = db.Auth_role.FirstOrDefault(p => p.Id.Equals(roleId));
            return result ?? throw new InvalidOperationException();
        }

        // Edit Auth_User
        public static void EditAuthUser(
                Auth_user oldUser,
                Guid id,
                string firstName,
                string lastName,
                string email,
                Guid newRole)
        {
            using var db = new ApplicationContext();
            var user = db.Auth_user.FirstOrDefault(u => u.Id == oldUser.Id);
            if (user != null)
            {
                user.Id = id;
                user.FirstName = firstName;
                user.LastName = lastName;
                user.Email = email;
                user.Role_Id = newRole;
            }

            db.SaveChanges();
        }
    }
}
