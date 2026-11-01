using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.DTOs
{
    public class DetalleFacturaDTO
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        
        public int ProductoId { get; set; }
        public string? Descripcion { get; set; }
        public int Cantidad { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }
        //public decimal SubTotal { get; set; }
        public decimal Descuento { get; set; }
    }
}
