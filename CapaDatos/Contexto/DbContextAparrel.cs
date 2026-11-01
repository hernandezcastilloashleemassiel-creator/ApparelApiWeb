using CapaEntidad;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Contexto
{
    public class DbContextAparrel : IdentityDbContext
    {
        public DbContextAparrel(DbContextOptions<DbContextAparrel> options) : base(options)
        {

        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Factura> Facturas { get; set; }

        public DbSet<DetalleFactura> DetalleFacturas { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }

        public DbSet<Inventario> Inventarios { get; set; }

    }


}
