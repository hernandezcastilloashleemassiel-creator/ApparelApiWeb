using CapaNegocio.Servicio.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos.Contexto;
using CapaEntidad;
using Microsoft.EntityFrameworkCore;
using CapaEntidad.DTOs;

namespace CapaNegocio.Servicio
{
    public class IInventarioServicio : IInventarioInterface
    {
        private readonly DbContextAparrel _dbContextApparel;
        private List<InventarioDTO> inventarios;

        public IInventarioServicio(DbContextAparrel dbContextApparel)
        {
            _dbContextApparel = dbContextApparel;
        }

        public async Task<bool> ActualizarInventario(InventarioDTO modelo)
        {
            var inventario = await _dbContextApparel.Inventarios
             .FirstOrDefaultAsync(i => i.IdInventario == modelo.IdInventario);

            if (inventario == null)
                return false;

            inventario.Nombre = modelo.Nombre;
            inventario.Descripcion = modelo.Descripcion;
            inventario.Precio = modelo.Precio;
            inventario.Stock = modelo.Stock;
            inventario.Categoria = modelo.Categoria;
            inventario.Estado = modelo.Estado;

            await _dbContextApparel.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EliminarInventario(int id)
        {
            var inventario = await _dbContextApparel.Inventarios
                  .FirstOrDefaultAsync(i => i.IdInventario == id);

            if (inventario == null)
                return false;

            _dbContextApparel.Inventarios.Remove(inventario);
            await _dbContextApparel.SaveChangesAsync();

            return true;

        }

        public async Task<InventarioDTO> InsertarInventario(InventarioDTO modelo)
        {
            var inventario = new Inventario
            {
                Nombre = modelo.Nombre,
                Descripcion = modelo.Descripcion,
                Precio = modelo.Precio,
                Stock = modelo.Stock,
                Categoria = modelo.Categoria,
                Estado = modelo.Estado
            };

            _dbContextApparel.Inventarios.Add(inventario);
            await _dbContextApparel.SaveChangesAsync();

            modelo.IdInventario = inventario.IdInventario;
            return modelo;

        }

        public async Task<List<InventarioDTO>> ListarInventario()
        {
            return await _dbContextApparel.Inventarios
                .Select(i => new InventarioDTO
                {
                    IdInventario = i.IdInventario,
                    Nombre = i.Nombre,
                    Descripcion = i.Descripcion,
                    Precio = i.Precio,
                    Stock = i.Stock,
                    Categoria = i.Categoria,
                    Estado = i.Estado
                })
                .ToListAsync();


        }

        public async Task<InventarioDTO> ObtenerInventarioPorId(int id)
        {
            var inventario = await _dbContextApparel.Inventarios
              .FirstOrDefaultAsync(i => i.IdInventario == id);

            if (inventario == null)
                return null;

            return new InventarioDTO
            {
                IdInventario = inventario.IdInventario,
                Nombre = inventario.Nombre,
                Descripcion = inventario.Descripcion,
                Precio = inventario.Precio,
                Stock = inventario.Stock,
                Categoria = inventario.Categoria,
                Estado = inventario.Estado
            };
        }


    }

}