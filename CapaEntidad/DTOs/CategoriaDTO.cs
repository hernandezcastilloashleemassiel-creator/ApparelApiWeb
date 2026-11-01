using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.DTOs
{
    public class CategoriaDTO
    {
        public int Id { get; set; }
        public string NombreCategoria { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; }
    }
}
