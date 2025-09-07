using System.Data.Entity;
using WinFormsEF6Demo.Models;

namespace WinFormsEF6Demo.Data
{
    public class AppDb : DbContext
    {
        public AppDb() : base("name=AppDb")
        {
            // BD existente: desactivar cualquier initializer
            Database.SetInitializer<AppDb>(null);
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Bodega> Bodegas { get; set; }
        public DbSet<Producto> Productos { get; set; }
    }
}
