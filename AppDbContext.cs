using Microsoft.EntityFrameworkCore;

namespace dotnetApiCode
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Pessoa> Pessoas { get; set; }

        
    }
}