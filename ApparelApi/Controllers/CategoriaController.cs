using CapaEntidad;
using CapaEntidad.DTOs;
using CapaNegocio.Servicio.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ApparelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase

    {
        private readonly ICategoriaServicio _categoriaServicio;
        public CategoriaController(ICategoriaServicio categoriaServicio)
        {
            this._categoriaServicio = categoriaServicio;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var categorias = await _categoriaServicio.ListarCategoria();
            return Ok(categorias);
        }

        [HttpPost]
        public async Task<IActionResult> Insertar([FromBody] CategoriaDTO categoria)
        {
            var productos = await _categoriaServicio.InsertarCategoria(categoria);
            if (!productos)
            {
                return BadRequest("No se pudo insertar la categoria");
            }
            return Ok(new { message = "La categoria se inserto correctamente" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] CategoriaDTO categoria)
        {
            var productoExistente = await _categoriaServicio.ObtenerCategoriaPorId(id);
            if (productoExistente == null)
                return NotFound("Categoria no encontrada");

            var categoriaActualizar = new Categoria
            {
                Id = id,
                NombreCategoria = categoria.NombreCategoria ?? string.Empty,
                Descripcion = categoria.Descripcion ?? string.Empty,
                Estado = categoria.Estado
            };

            var productos = await _categoriaServicio.ActualizarCategoria(categoriaActualizar);
            if (!productos)
            {
                return BadRequest("No se pudo actualizar la categoria");
            }
            return Ok(new { message = "la categoria se actualizo correctamente" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var categoria = await _categoriaServicio.ObtenerCategoriaPorId(id);
            if (categoria == null)
                return NotFound("categoria no encontrado");

            var result = _categoriaServicio.EliminarCategoria(categoria);
            if (!result)
            {
                return BadRequest("No se pudo eliminar la categoria");
            }
            return Ok(new { message = "La categoria se elimino correctamente" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var categoria = await _categoriaServicio.ObtenerCategoriaPorId(id);
            if (categoria == null)
                return NotFound("Categoría no encontrada");

            return Ok(categoria);
        }
    }
}
