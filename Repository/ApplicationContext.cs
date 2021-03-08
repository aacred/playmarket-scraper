using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<ApplicationDetails> ApplicationDetails { get; set; }
        
        public ApplicationContext() { }
        
        public ApplicationContext(DbContextOptions options) : base(options) { }
    }
}