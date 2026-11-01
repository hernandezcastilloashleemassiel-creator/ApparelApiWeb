using CapaEntidad.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public  class Factura
    {
        public int Id { get; set; }
        public int ? ClienteId { get; set; }
        public string? NumeroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        //public IEnumerable<DetalleFactura> DetalleFactura{ get; set; }

        public ICollection<DetalleFactura> DetalleFactura { get; set; }
    }
}
