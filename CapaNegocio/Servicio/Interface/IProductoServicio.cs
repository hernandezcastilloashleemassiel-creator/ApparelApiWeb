using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaEntidad.DTOs;

namespace CapaNegocio.Servicio.Interface
{
    public interface IProductoServicio
    {
        Task<List<ProductoDTO>> ListarProductos();
        Task<bool> InsertarProducto(ProductoDTO producto);
        Task<bool> ActualizarProducto(ProductoDTO producto);
        Task<ProductoDTO> ObtenerProductoPorId(int id);
        bool EliminarProducto(ProductoDTO producto);
        Task<bool> ActualizarProducto(Producto productoActualizar);
        Task<bool> ActualizarStockProductos(List<ActualizarStockDTO> productosVendidos);
   
    }
    public class ActualizarStockDTO
    {
        public int ProductoId { get; set; }
        public int CantidadVendida { get; set; }
    }
}
