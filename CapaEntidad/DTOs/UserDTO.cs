using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string CargoUsuario { get; set; }
        public string ContraseñaUsuario { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
