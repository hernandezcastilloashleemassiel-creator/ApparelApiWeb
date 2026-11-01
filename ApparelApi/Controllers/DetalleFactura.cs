using Microsoft.AspNetCore.Mvc;
using CapaNegocio.Servicio.Interface;
using CapaEntidad.DTOs;
using CapaEntidad;
using CapaNegocio.Servicio;
using Microsoft.AspNetCore.Http;
namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleFacturaController : ControllerBase
    {
        private readonly IDetalleFacturaServicio _detalleServicio;

        public DetalleFacturaController(IDetalleFacturaServicio detalleServicio)
        {
            _detalleServicio = detalleServicio;
        }

        // GET api/detallefactura/factura/5
        [HttpGet("factura/{facturaId}")]
        public async Task<IActionResult> ObtenerPorFactura(int facturaId)
        {
            var detalles = await _detalleServicio.ObtenerPorFactura(facturaId);
            return Ok(detalles);
        }

        // POST api/detallefactura
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] DetalleFacturaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var detalle = await _detalleServicio.Crear(dto);
            return Ok(detalle);
        }
    }
}
