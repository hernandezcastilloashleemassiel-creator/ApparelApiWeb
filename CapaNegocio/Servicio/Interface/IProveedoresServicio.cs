using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad.DTOs;
using CapaEntidad;

namespace CapaNegocio.Servicio.Interface
{
    public interface IProveedoresServicio
    {
        Task<List<ProveedoresDTO>> ListarProveedores();
        Task<ProveedoresDTO> ObtenerProveedorPorId(int id);

        Task<bool> InsertarProveedor(ProveedoresDTO proveedor);

        Task<bool> ActualizarProveedor(ProveedoresDTO proveedor);

        Task<bool> EliminarProveedor(ProveedoresDTO proveedor);
    }
}