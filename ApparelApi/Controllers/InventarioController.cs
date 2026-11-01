using CapaEntidad;
using CapaEntidad.DTOs;
using CapaNegocio.Servicio;
using CapaNegocio.Servicio.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApparelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {

        private readonly IInventarioServicio _inventarioServicio;

        public InventarioController(IInventarioServicio inventarioServicio)
        {
            _inventarioServicio = inventarioServicio;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var lista = await _inventarioServicio.ListarInventario();
            return Ok(lista);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var inventario = await _inventarioServicio.ObtenerInventarioPorId(id);

            if (inventario == null)
                return NotFound();

            return Ok(inventario);
        }

        [HttpPost]
        public async Task<IActionResult> Insertar([FromBody] InventarioDTO modelo)
        {
            var resultado = await _inventarioServicio.InsertarInventario(modelo);
            return Ok(resultado);
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] InventarioDTO modelo)
        {
            var resultado = await _inventarioServicio.ActualizarInventario(modelo);

            if (!resultado)
                return NotFound();

            return Ok("Inventario actualizado correctamente");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var resultado = await _inventarioServicio.EliminarInventario(id);

            if (!resultado)
                return NotFound();

            return Ok("Inventario eliminado correctamente");
        }



    }


}