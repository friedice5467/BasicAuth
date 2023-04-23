using Microsoft.EntityFrameworkCore;
using BasicAuth.Models;
using Microsoft.Extensions.Configuration;

namespace BasicAuth.DAL
{
    public class IdentityDbContext : DbContext
    {
        public DbSet<User> Registrations { get; set; }

        private readonly IConfiguration _configuration;

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("tbl_user");
        }
    }
}
