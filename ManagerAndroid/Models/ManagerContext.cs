using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ManagerAndroid.Models
{
    public class ManagerContext : IdentityDbContext<User>
    {
        public DbSet<Android> Androids { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Assignent> Assignents { get; set; }
       
        public ManagerContext(DbContextOptions<ManagerContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
