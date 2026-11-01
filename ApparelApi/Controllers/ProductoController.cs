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
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles ="Administrador")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoServicio _productoServicio;
        public ProductoController(IProductoServicio productoServicio)
        {
            this._productoServicio = productoServicio;
        }
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var productos = await _productoServicio.ListarProductos();
            return Ok(productos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var producto = await _productoServicio.ObtenerProductoPorId(id);

            if (producto == null)
            {
                return NotFound("Producto no encontrado");
            }

            return Ok(producto);
        }
        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
        public async Task<IActionResult> Insertar([FromBody] ProductoDTO producto)
        {
            var productos = await _productoServicio.InsertarProducto(producto);
            if (!productos)
            {
                return BadRequest("No se pudo insertar el producto");
            }
            return Ok(new {message = "El producto se inserto correctamente" });
        }

        [HttpPut("{id}")]
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]

        public async Task<IActionResult> Actualizar(int id, [FromBody] ProductoDTO producto)
        {
            var productoExistente = await _productoServicio.ObtenerProductoPorId(id);
            if (productoExistente == null)
                return NotFound("Producto no encontrado");

            var ProductoActualizar = new Producto
            {
                Id = id,
                CategoriaID = producto.CategoriaId,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                Activo = producto.Activo
            };
            var productos = await _productoServicio.ActualizarProducto(ProductoActualizar);
            if (!productos)
            {
                return BadRequest("No se pudo actualizar el producto");
            }
            return Ok(new { message = "El producto se actualizó correctamente" });
        }

        [HttpDelete("{id}")]
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]

        public async Task<IActionResult> Eliminar(int id)
        {
            var producto = await _productoServicio.ObtenerProductoPorId(id);
            if (producto == null)
                return NotFound("Producto no encontrado");

            var productos =  _productoServicio.EliminarProducto(producto);
            if (!productos)
            {
                return BadRequest("No se pudo eliminar el producto");
            }
            return Ok(new { message = "El producto se elimino correctamente" });
        }
    }
}
