using Microsoft.EntityFrameworkCore;
using EMS.API.Models;

namespace EMS.API.Data
{
    public class EmsContext : DbContext
    {
        public EmsContext(DbContextOptions<EmsContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
