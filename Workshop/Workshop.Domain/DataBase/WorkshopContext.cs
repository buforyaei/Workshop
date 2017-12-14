using Microsoft.EntityFrameworkCore;
using Workshop.Domain.Models;

namespace Workshop.Domain.DataBase
{
    public class WorkshopContext : DbContext
    {
        public WorkshopContext() { }

        public WorkshopContext(DbContextOptions<WorkshopContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:workshopdbserver.database.windows.net,1433;Initial Catalog=workshopdb;Persist Security Info=False;User ID=tyt66;Password=Logo6666;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
        public DbSet<WorkshopTask> Tasks { get; set; }
        public DbSet<WorkshopProblem> Problems { get; set; }
        public DbSet<WorkshopObject> Objects { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<UserAccount> UsersAccounts { get; set; }
    }
}
