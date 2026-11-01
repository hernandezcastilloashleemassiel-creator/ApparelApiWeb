using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.DTOs
{
    public class ProveedoresDTO
    {
        public int Id { get; set; }
        public string Empresa { get; set; }
        public string Vendedor { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

    }
}