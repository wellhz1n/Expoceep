using Expoceep.Models;
using Microsoft.EntityFrameworkCore;

namespace Expoceep.DB
{
    public class ERPDatabaseContext : DbContext
    {
        
        public ERPDatabaseContext(DbContextOptions<ERPDatabaseContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Usuario>().HasIndex(u => u.Email).IsUnique();
            builder.Entity<Usuario>().HasIndex(u => u.Login).IsUnique();
         
        }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
