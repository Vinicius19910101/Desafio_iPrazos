using API_Rest.Controllers;
using Microsoft.EntityFrameworkCore;

namespace API_Rest.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<API_Rest.Models.JurisprudenciaModel> Jurisprudencias { get; set; }
    }
}
