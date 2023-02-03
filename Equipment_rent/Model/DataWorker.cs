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
            return db.Users.OrderBy(o => o.Name).ToList();
        }

        // Get All Equipments
        public static List<Equipment> GetAllEquipments()
        {
            using var db = new ApplicationContext();
            return db.Equipments.OrderBy(o => o.TypeId).ToList();
        }

        //Get All Orders
        public static List<Order> GetAllOrders()
        {
            using var db = new ApplicationContext();
            return db.Orders.OrderBy(o => o.IsReturned).ToList();
        }

        //Get All Types
        public static List<Type> GetAllTypes()
        {
            using var db = new ApplicationContext();
            return db.Types.ToList();
        }

        // Get All Auth_users
        public static List<Auth_user> GetAllAuthUsers()
        {
            using var db = new ApplicationContext();
            return db.Auth_user.OrderBy(o => o.Username).ToList();
        }

        // Get All Roles
        public static List<Auth_role> GetAllAuthRoles()
        {
            using var db = new ApplicationContext();
            return db.Auth_role.ToList();
        }

        // Add User
        public static User CreateUser(string name, string phone, bool debt)
        {
            using var db = new ApplicationContext();
            if (!db.Users.Any(el => el.Phone == phone && el.Name == name))
            {
                var newUser = new User { Name = name, Phone = phone, Debt = debt };
                db.Users.Add(newUser);
                db.SaveChanges();
                return newUser;
            }
            return null;
        }

        // Add Equipment
        public static void CreateEquip(Type type, string model, int amount)
        {
            using var db = new ApplicationContext();
            if (!db.Equipments.Any(el => el.Model == model))
            {
                db.Equipments.Add(new Equipment
                {
                    TypeId = type.TypeId,
                    Model = model,
                    Amount = amount,
                    Balance = amount
                });
                db.SaveChanges();
            }
        }

        // Add Order
        public static void CreateOrder(User user, Equipment equipment, int amount, DateTime dateIssue, DateTime dateReturn, Guid whoGive)
        {
            using var db = new ApplicationContext();
            db.Orders.Add(new Order
            {
                UserId = user.UserId,
                EquipmentId = equipment.EquipmentId,
                Amount = amount,
                DateIssue = dateIssue,
                DateReturn = dateReturn,
                IsReturned = false,
                WhoGive = whoGive
            });
            db.SaveChanges();
        }

        // Add Staff
        public static void CreateStaff(Guid userId, string username, string firstname, string lastname, string email,
            Guid roleId)
        {
            using var db = new ApplicationContext();
            if (!db.Auth_user.Any(el => el.Username == username && el.Email == email))
            {
                db.Auth_user.Add(new Auth_user
                {
                    Id = userId,
                    Username = username,
                    FirstName = firstname,
                    LastName = lastname,
                    Email = email,
                    Role_Id = roleId,
                    Auth_roleId = roleId
                });
                db.SaveChanges();
            }
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
                db.SaveChanges();
            }
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
                db.SaveChanges();
            }
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
                db.SaveChanges();
            }
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
                db.SaveChanges();
            }
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
            return db.Types.FirstOrDefault(p => p.TypeId == id) ??
                   throw new InvalidOperationException("Type not found");
        }

        // Get Equipment by Id
        public static Equipment GetEquipmentById(int id)
        {
            using var db = new ApplicationContext();
            return db.Equipments.FirstOrDefault(p => p.EquipmentId == id) ??
                   throw new InvalidOperationException("Equipment ID not found");
        }

        // Get User by Id
        public static User GetUserById(int id)
        {
            using var db = new ApplicationContext();
            return db.Users.FirstOrDefault(p => p.UserId == id) ??
                   throw new InvalidOperationException("User ID not found");
        }

        // Get User By Name
        public static User GetUserByName(string name)
        {
            using var db = new ApplicationContext();
            return db.Users.FirstOrDefault(p => p.Name == name) ??
                   throw new InvalidOperationException("User Name not found");
        }

        // Get All Equipments by Id Type
        public static List<Equipment> GetAllEquipmentsByIdType(int id)
        {
            return GetAllEquipments().Where(equipment => equipment.TypeId == id).ToList();
        }

        // Get All Orders by Id User
        public static List<Order> GetAllOrdersByUserId(int id)
        {
            return GetAllOrders().Where(order => order.UserId == id && order.IsReturned == false).ToList();
        }

        // Get All Orders by Id Equipment
        public static List<Order> GetAllOrdersByEquipmentId(int id)
        {
            return GetAllOrders().Where(order => order.EquipmentId == id && order.IsReturned == false).ToList();
        }

        // Get all orders by status
        public static List<Order> GetAllOrdersByStatus()
        {
            return GetAllOrders().Where(order => order.IsReturned == false).ToList();
        }

        // Get AuthUser By Id
        public static Auth_user GetAuthUserById(Guid id)
        {
            using var db = new ApplicationContext();
            return db.Auth_user.FirstOrDefault(p => p.Id.Equals(id)) ?? 
                   throw new InvalidOperationException("Staff ID not found");
        }

        // Get Role By Id
        public static Auth_role GetRoleById(Guid roleId)
        {
            using var db = new ApplicationContext();
            return db.Auth_role.FirstOrDefault(p => p.Id.Equals(roleId)) ?? 
                   throw new InvalidOperationException("Auth Role ID not found");
        }

        // Get Preview Users
        public static List<User> GetPreviousPageUsers(int pageIndex, int count)
        {
            pageIndex = Math.Max(pageIndex - 1, 1);
            UsersVM.pageIndex = pageIndex;

            var skip = (pageIndex - 1) * count;
            var users = GetAllUsers();
            UsersVM.count = Math.Min(skip + count, users.Count);

            return users.Skip(skip).Take(count).ToList();
        }

        // Get Preview Equipments
        public static List<Equipment> GetPreviousPageEquipments(int pageIndex, int count)
        {
            pageIndex = Math.Max(pageIndex - 1, 1);
            EquipmentsVM.pageIndex = pageIndex;

            var skip = (pageIndex - 1) * count;
            var equipments = GetAllEquipments();
            EquipmentsVM.count = Math.Min(skip + count, equipments.Count);

            return equipments.Skip(skip).Take(count).ToList();
        }

        // Get Preview Orders
        public static List<Order> GetPreviousPageOrders(int pageIndex, int count)
        {
            pageIndex = Math.Max(pageIndex - 1, 1);
            OrdersVM.pageIndex = pageIndex;

            var skip = (pageIndex - 1) * count;
            var orders = GetAllOrders();
            OrdersVM.count = Math.Min(skip + count, orders.Count);

            return orders.Skip(skip).Take(count).ToList();
        }

        // Get Preview Staff
        public static List<Auth_user> GetPreviousPageStaff(int pageIndex, int count)
        {
            pageIndex = Math.Max(pageIndex - 1, 1);
            StaffVM.pageIndex = pageIndex;

            var skip = (pageIndex - 1) * count;
            var staff = GetAllAuthUsers();
            StaffVM.count = Math.Min(skip + count, staff.Count);

            return staff.Skip(skip).Take(count).ToList();
        }

        // Get Next Users
        public static List<User> GetNextPageUsers(int pageIndex, int count)
        {
            pageIndex = Math.Max(pageIndex + 1, 1);
            UsersVM.pageIndex = pageIndex;
            var result = GetAllUsers().Skip((pageIndex - 1) * count).Take(count).ToList();
            UsersVM.count = Math.Min(pageIndex * count, GetAllUsers().Count);
            return result;
        }

        // Get Next Equipments
        public static List<Equipment> GetNextPageEquipments(int pageIndex, int count)
        {
            pageIndex = Math.Max(pageIndex + 1, 1);
            EquipmentsVM.pageIndex = pageIndex;
            var result = GetAllEquipments().Skip((pageIndex - 1) * count).Take(count).ToList();
            EquipmentsVM.count = Math.Min(pageIndex * count, GetAllEquipments().Count);
            return result;
        }

        // Get next Orders
        public static List<Order> GetNextPageOrders(int pageIndex, int count)
        {
            pageIndex = Math.Max(pageIndex + 1, 1);
            OrdersVM.pageIndex = pageIndex;
            var result = GetAllOrders().Skip((pageIndex - 1) * count).Take(count).ToList();
            OrdersVM.count = Math.Min(pageIndex * count, GetAllOrders().Count);
            return result;
        }

        // Get Next Staff
        public static List<Auth_user> GetNextPageStaff(int pageIndex, int count)
        {
            pageIndex = Math.Max(pageIndex + 1, 1);
            StaffVM.pageIndex = pageIndex;
            var result = GetAllAuthUsers().Skip((pageIndex - 1) * count).Take(count).ToList();
            StaffVM.count = Math.Min(pageIndex * count, GetAllAuthUsers().Count);
            return result;
        }

        //Get First Users
        public static List<User> GetFirstUsers(int count)
        {
            return GetAllUsers().Take(count).ToList();
        }

        //Get First Equipments
        public static List<Equipment> GetFirstEquipments(int count)
        {
            return GetAllEquipments().Take(count).ToList();
        }

        //Get First Orders
        public static List<Order> GetFirstOrders(int count)
        {
            return GetAllOrders().Take(count).ToList();
        }

        //Get First Staff
        public static List<Auth_user> GetFirstStaff(int count)
        {
            return GetAllAuthUsers().Take(count).ToList();
        }
    }
}