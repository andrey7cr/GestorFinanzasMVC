using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GestorFinanzasMVC.Models
{
    public class GestorFinanzasDbContext : DbContext
    {
        public GestorFinanzasDbContext(DbContextOptions<GestorFinanzasDbContext> options)
        : base(options)
        { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
