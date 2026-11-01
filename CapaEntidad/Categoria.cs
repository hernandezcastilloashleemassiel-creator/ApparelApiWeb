using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    [Table("Categorias")]
    public class Categoria

    {
        public int Id { get; set; }
        public string NombreCategoria { get; set; }
        public string Descripcion { get; set; }
        public int  Estado { get; set; }
        public IEnumerable<Producto> Productos { get; set; }
    }
}
