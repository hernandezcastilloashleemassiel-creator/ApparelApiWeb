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
    public class ProductoServicio : IProductoServicio
    {
        private readonly DbContextAparrel _dbContextApparel;
        public ProductoServicio(DbContextAparrel contextAparrel)
        {
            this._dbContextApparel = contextAparrel;
        }
        async Task<bool> IProductoServicio.InsertarProducto(ProductoDTO producto)
        {
            var nuevoProducto = new Producto
            {
                Id = producto.Id,
                CategoriaID = producto.CategoriaId,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Activo = producto.Activo,
                Stock = producto.Stock
            };
            await _dbContextApparel.Productos.AddAsync(nuevoProducto);
            return await ConfimarCambios();
        }


        public async Task<ProductoDTO?> ObtenerProductoPorId(int id)
        {
            var producto = await _dbContextApparel.Productos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (producto == null)
                return null;

            return new ProductoDTO
            {
                Id = producto.Id,
                CategoriaId = producto.CategoriaID,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Activo = producto.Activo,
                Stock = producto.Stock


            };
        }


        public async Task<bool> ConfimarCambios()
        {
            bool esGuardado = false;
            var result = await _dbContextApparel.SaveChangesAsync();
            if (result > 0)
                esGuardado = true;
            return esGuardado;
        }

        public async Task<List<ProductoDTO>> ListarProductos()
        {
            var listaProducto = new List<ProductoDTO>();
            var productos = await _dbContextApparel.Productos.Include(p => p.Categoria).ToListAsync();
            foreach (var item in productos)
            {
                listaProducto.Add(new ProductoDTO
                {
                    Id = item.Id,
                    CategoriaId = item.CategoriaID,
                    Nombre = item.Nombre,
                    Descripcion = item.Descripcion,
                    Precio = item.Precio,
                    Stock = item.Stock,
                    Activo = item.Activo,
                    NombreCategoria = item.Categoria != null ? item.Categoria.NombreCategoria : "Sin Categoria"


                });
            }
            return listaProducto;
        }

        bool IProductoServicio.EliminarProducto(ProductoDTO producto)
        {
            var productoEntity = new Producto
            {
                Id = producto.Id,
                CategoriaID = producto.CategoriaId,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Activo = producto.Activo,
                Stock = producto.Stock


            };
            _dbContextApparel.Productos.Remove(productoEntity);
            // Guardar cambios y devolver el resultado de forma sincrónica
            return _dbContextApparel.SaveChanges() > 0;
        }

        public Task<bool> ActualizarProducto(Producto productoActualizar)
        {
            _dbContextApparel.Productos.Update(productoActualizar);
            return ConfimarCambios();
        }

        public async Task<bool> ActualizarProducto(ProductoDTO producto)
        {
            var productoEntidad = new Producto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                CategoriaID = producto.CategoriaId,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Activo = producto.Activo,
                Stock = producto.Stock


            };

            _dbContextApparel.Productos.Update(productoEntidad);
            return await ConfimarCambios();
        }

        public async Task<bool> ActualizarStockProductos(List<ActualizarStockDTO> productosVendidos)
        {
            foreach (var item in productosVendidos)
            {
                var producto = await _dbContextApparel.Productos.FirstOrDefaultAsync(p => p.Id == item.ProductoId);
                if (producto == null)
                {
                    // Producto no encontrado, puedes manejarlo como error o continuar
                    return false;
                }

                if (producto.Stock < item.CantidadVendida)
                {
                    // No hay suficiente stock
                    return false;
                }

                producto.Stock -= item.CantidadVendida;
            }

            // Guardar cambios una sola vez fuera del ciclo
            var resultado = await _dbContextApparel.SaveChangesAsync();
            return resultado > 0;
        }
    }
}
    //        return BadRequest("No se pudo eliminar el producto");

