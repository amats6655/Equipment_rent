using Equipment_rent.Utilites;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Windows.Markup;

namespace Equipment_rent.Model
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Equipment> Equipments { get; set; }

        public DbSet<Auth_user> Auth_user { get; set; }
        public DbSet<Auth_role> Auth_role { get; set; }

        public ApplicationContext()
        {

            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = DESKTOP-Q1BU01R\\SQLEXPRESS; Initial Catalog = ER; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message));
        }
    } 


}
