using Microsoft.EntityFrameworkCore;

namespace OrganizingEvents.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        { }
    }
}