using System;
using CapaEntidad;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad.DTOs;

namespace CapaNegocio.Servicio.Interface
{
    public interface IFacturaServicio
    {
        Task<MessageDTO> CrearFactura(FacturaDTO factura);

        Task<IEnumerable<FacturaDTO>> ObtenerTodas();
    }

  
       
       


}


