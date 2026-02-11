using Microsoft.EntityFrameworkCore;
using Estacionamento.Models;

namespace Estacionamento.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Veiculo> VEICULO { get; set; }
        public DbSet<Tarifa> TARIFA { get; set; }

    }    
}