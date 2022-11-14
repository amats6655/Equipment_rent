using Microsoft.EntityFrameworkCore;

namespace Equipment_rent.Model
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Equipment> Equipments { get; set; }

        public ApplicationContext()
        {

            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=85.175.4.135,49172\\SQLEXPRESS;DataBase=equipment_rent_work;User ID=mrkotik;Password=ciscO15=!");
        }
    } 


}
