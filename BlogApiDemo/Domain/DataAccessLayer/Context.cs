using Microsoft.EntityFrameworkCore;

namespace BlogApiDemo.Domain.DataAccessLayer
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MELIH\\SQLEXPRESS;Initial Catalog=CoreBlogApiDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        public DbSet<Employee> Employees { get; set; }


    }
}
