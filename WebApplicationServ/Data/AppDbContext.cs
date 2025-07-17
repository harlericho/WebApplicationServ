using Microsoft.EntityFrameworkCore;
using WebApplicationServ.Models;

namespace WebApplicationServ.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Perso> Persos => Set<Perso>();
        public DbSet<RespuestaXEmpresa> RespuestasXEmpresa => Set<RespuestaXEmpresa>();

    }
}
