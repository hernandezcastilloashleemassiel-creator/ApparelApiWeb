using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad.DTOs;

namespace CapaNegocio.Servicio.Interface
{
    public interface IInventarioInterface
    {
        Task<bool> ActualizarInventario(InventarioDTO modelo);

        Task<InventarioDTO> InsertarInventario(InventarioDTO modelo);

        Task<bool> EliminarInventario(int id);

        Task<List<InventarioDTO>> ListarInventario();

        Task<InventarioDTO> ObtenerInventarioPorId(int id);


    }
}