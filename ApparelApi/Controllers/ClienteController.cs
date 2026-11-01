using CapaEntidad;
using CapaEntidad.DTOs;
using CapaNegocio.Servicio.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApparelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
         
        private readonly IClienteServicio _clienteServicio;
        public ClienteController(IClienteServicio clienteServicio)
        {
            this._clienteServicio = clienteServicio;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var clientes = await _clienteServicio.ListarCliente();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var cliente = await _clienteServicio.ObtenerClientePorId(id);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Insertar([FromBody] ClienteDTO cliente)
        {
            var client = await _clienteServicio.InsertarCliente(cliente);
            if (!client)
            {
                return BadRequest("No se pudo insertar al cliente");
            }
            return Ok(new { message = "El cliente se inserto correctamente" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizarcliente(int id, [FromBody] ClienteDTO cliente)
        {
            var clienteExistente = await _clienteServicio.ObtenerClientePorId(id);
            if (clienteExistente == null)
                return NotFound("Cliente no encontrado");

            var clienteActualizar = new Cliente
            {
                Id = id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Email = cliente.Email,
                Telefono = cliente.Telefono,
                Activo = cliente.Activo
            };

            var client = await _clienteServicio.ActualizarCliente(clienteActualizar);
            if (!client)
            {
                return BadRequest("No se pudo actualizar al cliente");
            }
            return Ok(new { message = "El cliente se actualizo correctamente" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCliente(int id)
        {
            var cliente = await _clienteServicio.ObtenerClientePorId(id);
            if (cliente == null)
                return NotFound("cliente no encontrado");

            var result = _clienteServicio.EliminarCliente(cliente);
            if (!result)
            {
                return BadRequest("No se pudo eliminar al cliente");
            }
            return Ok(new { message = "El cliente se elimino correctamente" });
        }
    }
}

