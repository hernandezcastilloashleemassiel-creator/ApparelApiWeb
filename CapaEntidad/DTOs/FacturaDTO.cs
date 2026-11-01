using System;
using System.Collections.Generic;

namespace CapaEntidad.DTOs
{
    public class FacturaDTO
    {
        public int Id { get; set; }
        public int? ClienteId { get; set; }
        public DateTime Fecha { get; set; }

        public decimal Total { get; set; }

        public List<DetalleFacturaDTO> DetalleFacturaDTOs { get; set; }
            = new List<DetalleFacturaDTO>();
    }
}