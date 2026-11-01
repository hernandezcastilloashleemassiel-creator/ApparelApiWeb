using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.DTOs
{
    public class InventarioDTO
    {
        public int IdInventario { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public string NombreProducto { get; set; }

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public string Categoria { get; set; }

        public bool Estado { get; set; }

    }
}