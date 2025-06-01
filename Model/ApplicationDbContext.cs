using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OrgManager_API.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=OrgManager-API_DB;Integrated Security=True;");
        //}
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }

    }
}