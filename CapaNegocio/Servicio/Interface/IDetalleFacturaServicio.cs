using CapaEntidad;
using CapaEntidad.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapaNegocio.Servicio.Interface
{
    public interface IDetalleFacturaServicio
    {
        Task<IEnumerable<DetalleFacturaDTO>> ObtenerPorFactura(int facturaId);
        Task<DetalleFactura> Crear(DetalleFacturaDTO dto);
    }
}
