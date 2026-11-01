using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using CapaEntidad.DTOs;
using CapaNegocio.Servicio;
using CapaNegocio.Servicio.Interface;
using CapaEntidad;
namespace ApparelApi.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedoresServicio _proveedoresServicio;

        // Inyectamos el servicio mediante el constructor
        public ProveedorController(IProveedoresServicio proveedoresServicio)
        {
            this._proveedoresServicio = proveedoresServicio;
        }

        // 1. GET: api/Proveedores (Listar todos)
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var proveedores = await _proveedoresServicio.ListarProveedores();
            return Ok(proveedores);
        }

        // 2. GET: api/Proveedores/{id} (Obtener uno solo)
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var proveedor = await _proveedoresServicio.ObtenerProveedorPorId(id);
            if (proveedor == null)
            {
                return NotFound(new { mensaje = $"Proveedor con ID {id} no fue encontrado." });
            }
            return Ok(proveedor);
        }

        // 3. POST: api/Proveedores (Crear nuevo)
        [HttpPost]
        public async Task<IActionResult> Insertar([FromBody] ProveedoresDTO proveedorDto)
        {
            if (proveedorDto == null) return BadRequest();

            var resultado = await _proveedoresServicio.InsertarProveedor(proveedorDto);
            if (resultado)
            {
                return Ok(new { mensaje = "Proveedor registrado exitosamente." });
            }
            return BadRequest(new { mensaje = "No se pudo registrar el proveedor." });
        }

        // 4. PUT: api/Proveedores (Actualizar existente)
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ProveedoresDTO proveedorDto)
        {
            if (proveedorDto == null)
                return BadRequest();

            proveedorDto.Id = id;

            var resultado = await _proveedoresServicio.ActualizarProveedor(proveedorDto);

            if (resultado)
            {
                return Ok(new { mensaje = "Proveedor actualizado de forma correcta." });
            }

            return NotFound(new { mensaje = "No se encontró el proveedor para actualizar." });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var proveedorDto = new ProveedoresDTO { Id = id };

            var resultado = await _proveedoresServicio.EliminarProveedor(proveedorDto);
            if (resultado)
            {
                return Ok(new { mensaje = "Proveedor eliminado con éxito." });
            }
            return NotFound(new { mensaje = "El proveedor que intenta eliminar no existe." });
        }
    }

}