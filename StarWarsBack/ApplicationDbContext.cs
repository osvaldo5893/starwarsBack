using Microsoft.EntityFrameworkCore;
using StarWarsBack.Models;

namespace StarWarsBack
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Usuario> usuario { get; set; }

    }   
}
