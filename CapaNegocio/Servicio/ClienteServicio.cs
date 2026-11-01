using CapaDatos.Contexto;
using CapaNegocio.Servicio.Interface;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CapaEntidad.DTOs;

namespace CapaNegocio.Servicio
{
    public class ClienteServicio : IClienteServicio

    {
        private readonly DbContextAparrel _dbContextAparrel;
        public ClienteServicio(DbContextAparrel contextAparrel)
        {
            this._dbContextAparrel = contextAparrel;
        }


        public async Task<List<ClienteDTO>> ListarCliente()
        {
            var listaCliente = new List<ClienteDTO>();
            var cliente = await _dbContextAparrel.Clientes.ToListAsync();
            foreach (var item in cliente)
            {
                listaCliente.Add(new ClienteDTO { Id = item.Id, Nombre = item.Nombre, Apellido = item.Apellido, Email = item.Email, Telefono = item.Telefono, Activo =item.Activo });
            }

            return listaCliente;
        }

        public async Task<bool> ActualizarCliente(ClienteDTO cliente)
        {
            var clienteEntidad = new Cliente
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Email = cliente.Email,
                Telefono = cliente.Telefono,
                Activo = cliente.Activo
            };
            _dbContextAparrel.Clientes.Update(clienteEntidad);
            return await ConfirmarCambios();
        }

        public async Task<ClienteDTO> ObtenerClientePorId(int id)
        {
            var cliente = await _dbContextAparrel.Clientes.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (cliente == null)
                return null;

            return new ClienteDTO
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Email = cliente.Email,
                Telefono = cliente.Telefono,
                Activo = cliente.Activo

            };
        }

        public bool EliminarCliente(ClienteDTO cliente)
        {
            var client = new Cliente { Id = cliente.Id, Nombre = cliente.Nombre, Apellido = cliente.Apellido, Email = cliente.Email, Telefono =cliente.Telefono };
            _dbContextAparrel.Clientes.Remove(client);
            var result = _dbContextAparrel.SaveChanges();
            if (result > 0)
                return true;
            return false;
        }
        public async Task<bool> ConfirmarCambios()
        {
            bool esGuardado = false;
            var result = await _dbContextAparrel.SaveChangesAsync();
            if (result > 0)
                esGuardado = true;
            return esGuardado;
        }

        async Task<bool> IClienteServicio.InsertarCliente(ClienteDTO cliente)
        {
            var nuevoCliente = new Cliente
            {
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Email = cliente.Email,
                Telefono = cliente.Telefono,
            };
            await _dbContextAparrel.Clientes.AddAsync(nuevoCliente);
            var result = await _dbContextAparrel.SaveChangesAsync();
            return result > 0;
        }

        public Task<bool> ActualizarCliente(Cliente clienteActualizar)
        {
            _dbContextAparrel.Clientes.Update(clienteActualizar);
            return ConfirmarCambios();

        }
    }
}
