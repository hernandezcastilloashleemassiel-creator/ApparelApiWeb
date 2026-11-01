using CapaEntidad;
using CapaEntidad.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Servicio.Interface
{
    public interface ICategoriaServicio
    {
        Task<List<CategoriaDTO>> ListarCategoria();
        Task<bool> InsertarCategoria(CategoriaDTO categoria);
        Task<bool> ActualizarCategoria(Categoria categoria);
        Task<CategoriaDTO> ObtenerCategoriaPorId(int id);
        bool EliminarCategoria(CategoriaDTO categoria);
    }
}
