using CapaEntidad.DTOs;
using CapaNegocio.Servicio.Interface;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class FacturaController : ControllerBase
{
    private readonly IFacturaServicio _facturaServicio;

    public FacturaController(IFacturaServicio facturaServicio)
    {
        _facturaServicio = facturaServicio;
    }

    // GET para listar todas las facturas hacia Flutter
    [HttpGet]
    public async Task<IActionResult> ObtenerTodas()
    {
        var facturas = await _facturaServicio.ObtenerTodas();
        return Ok(facturas);
    }

    // POST para crear una factura nueva
    [HttpPost]
    public async Task<IActionResult> CrearFactura([FromBody] FacturaDTO facturaDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var resultado = await _facturaServicio.CrearFactura(facturaDto);

        if (!resultado.IsSuccess)
            return BadRequest(resultado);

        return Ok(resultado);
    }
}