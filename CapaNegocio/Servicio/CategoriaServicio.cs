using CapaDatos.Contexto;
using CapaEntidad;
using CapaEntidad.DTOs;
using CapaNegocio.Servicio.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Servicio
{
    public class CategoriaServicio : ICategoriaServicio
    {
        private readonly DbContextAparrel _dbContextAparrel;
        public CategoriaServicio(DbContextAparrel contextAparrel)
        {
            this._dbContextAparrel = contextAparrel;
        }

        public Task<bool> ActualizarCategoria(Categoria categoria)
        {
            _dbContextAparrel.Categorias.Update(categoria);
            return ConfirmarCambios();
        }

        public bool EliminarCategoria(CategoriaDTO categoria)
        {
            var category = new Categoria { Id = categoria.Id, NombreCategoria = categoria.NombreCategoria,Descripcion = categoria.Descripcion, Estado = categoria.Estado };
            _dbContextAparrel.Categorias.Remove(category);
            var result = _dbContextAparrel.SaveChanges();
            if (result > 0)
                return true;
            return false;
        }
        public async Task<bool> InsertarCategoria(CategoriaDTO categoria)
        {
            var nuevaCategoria = new Categoria
            {
                NombreCategoria = categoria.NombreCategoria,
                Descripcion = categoria.Descripcion,
                Estado = categoria.Estado
            };
            await _dbContextAparrel.Categorias.AddAsync(nuevaCategoria);
            return await ConfirmarCambios();
        }

        public async Task<List<CategoriaDTO>> ListarCategoria()
        {
            var listaCategoria = new List<CategoriaDTO>();
            var categorias = await _dbContextAparrel.Categorias.ToListAsync();
            foreach (var item in categorias)
            {
                listaCategoria.Add(new CategoriaDTO { Id = item.Id, NombreCategoria = item.NombreCategoria, Descripcion = item.Descripcion , Estado = item.Estado });
            }

            return listaCategoria;
        }

        public async Task<CategoriaDTO> ObtenerCategoriaPorId(int id)
        {

            var categoria = await _dbContextAparrel.Categorias.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (categoria == null)
                return null;

            return new CategoriaDTO
            {
                Id = categoria.Id,
                NombreCategoria = categoria.NombreCategoria,
                Descripcion = categoria.Descripcion,
                Estado = categoria.Estado
            };
        }

        public async Task<bool> ConfirmarCambios()
        {
            bool esGuardado = false;
            var result = await _dbContextAparrel.SaveChangesAsync();
            if (result > 0)
                esGuardado = true;
            return esGuardado;
        }
    }
}
