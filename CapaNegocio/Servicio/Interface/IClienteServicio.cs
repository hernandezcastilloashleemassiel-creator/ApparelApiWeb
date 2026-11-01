using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad.DTOs;

namespace CapaNegocio.Servicio.Interface
{
    public interface IClienteServicio
    {
        Task<List<ClienteDTO>> ListarCliente();
        Task<bool> InsertarCliente(ClienteDTO cliente);
        Task<bool> ActualizarCliente(ClienteDTO cliente);
        Task<ClienteDTO> ObtenerClientePorId(int id);
        bool EliminarCliente(ClienteDTO cliente);
        Task<bool> ActualizarCliente(Cliente clienteActualizar);
    }
}
