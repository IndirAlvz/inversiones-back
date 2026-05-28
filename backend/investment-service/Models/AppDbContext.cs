using Microsoft.EntityFrameworkCore;
using investment_service.Models;

namespace investment_service
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<SecUsuario> SecUsuarios { get; set; }
    }
}