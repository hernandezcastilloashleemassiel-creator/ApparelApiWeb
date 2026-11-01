using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad.DTOs;

namespace CapaEntidad
{
    public  class Producto
    {
        public int Id { get; set; }
        public int CategoriaID { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool Activo { get; set; }
        public Categoria Categoria { get; set; }

        public int Stock { get; set; }
        public IEnumerable <DetalleFactura> DetalleFactura  { get; set; }

     //public static implicit operator Producto (Producto v) => throw new NotImplementedException();
    }
}
                                                        