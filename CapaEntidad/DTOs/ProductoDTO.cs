using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        public string ? NombreCategoria { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public bool Activo { get; set; }

    }
}
