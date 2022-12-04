using Microsoft.EntityFrameworkCore;

namespace Auth.Model
{
    public class ApplicationContext:DbContext
    {
        public DbSet<User> User { get; set; }

        public ApplicationContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=85.175.4.135,49172\\SQLEXPRESS;DataBase=equipment_rent_auth;User ID=mrkotik;Password=ciscO15=!TrustServerCertificate=true;");
        }
    }
}
