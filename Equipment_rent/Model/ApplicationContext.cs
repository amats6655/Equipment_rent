﻿using Microsoft.EntityFrameworkCore;

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
            optionsBuilder.UseSqlServer("Data Source=!***!;Initial Catalog=ER;User ID=!***!;Password=!***!;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }


}
