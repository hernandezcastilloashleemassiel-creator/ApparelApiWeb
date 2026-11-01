using CapaEntidad.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApparelApi.Controllers
{
    public class LoginDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public UsuarioController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        // REGISTRAR USUARIO
        [HttpPost("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] UserDTO userDTO)
        {
            var correo = string.IsNullOrWhiteSpace(userDTO.CorreoUsuario)
                ? userDTO.Email
                : userDTO.CorreoUsuario;

            if (string.IsNullOrWhiteSpace(correo) ||
                string.IsNullOrWhiteSpace(userDTO.Password) ||
                string.IsNullOrWhiteSpace(userDTO.CargoUsuario))
            {
                return BadRequest("Todos los campos son obligatorios.");
            }

            if (userDTO.CargoUsuario != "Administrador" &&
                userDTO.CargoUsuario != "Vendedor")
            {
                return BadRequest("El cargo seleccionado no es válido.");
            }

            var usuario = new IdentityUser
            {
                UserName = string.IsNullOrWhiteSpace(userDTO.NombreUsuario)
                    ? correo
                    : userDTO.NombreUsuario,
                Email = correo
            };

            var result = await _userManager.CreateAsync(
                usuario, userDTO.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            if (!await _roleManager.RoleExistsAsync("Administrador"))
            {
                await _roleManager.CreateAsync(
                    new IdentityRole("Administrador"));
            }

            if (!await _roleManager.RoleExistsAsync("Vendedor"))
            {
                await _roleManager.CreateAsync(
                    new IdentityRole("Vendedor"));
            }

            var rolResult = await _userManager.AddToRoleAsync(
                usuario, userDTO.CargoUsuario);

            if (!rolResult.Succeeded)
            {
                await _userManager.DeleteAsync(usuario);
                return BadRequest(rolResult.Errors);
            }

            return Ok("Usuario creado correctamente.");
        }

        // INICIAR SESIÓN
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO userDTO)
        {
            var user = await _userManager.FindByEmailAsync(userDTO.Email);

            if (user == null)
                return BadRequest("Correo o contraseña incorrectos.");

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName ?? string.Empty,
                userDTO.Password,
                false,
                false
            );

            if (!result.Succeeded)
                return BadRequest("Correo o contraseña incorrectos.");

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new UserTokenDTO
            {
                Token = GenerateJwtToken(user, roles),
                UserName = user.UserName ?? string.Empty,
                Roles = roles.ToList()
            });
        }

        // GENERAR TOKEN JWT
        private string GenerateJwtToken(
            IdentityUser user,
            IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.Name,
                    user.UserName ?? string.Empty),

                new Claim(
                    ClaimTypes.Email,
                    user.Email ?? string.Empty)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtkey = _configuration["Jwt:Key"];

            if (string.IsNullOrEmpty(jwtkey))
                throw new InvalidOperationException("No se encontró Jwt:Key.");

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtkey));

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // LISTAR USUARIOS
        [HttpGet("Lista")]
        public async Task<IActionResult> Lista()
        {
            var usuarios = _userManager.Users.ToList();
            var lista = new List<object>();

            foreach (var user in usuarios)
            {
                var roles = await _userManager.GetRolesAsync(user);

                lista.Add(new
                {
                    Id = user.Id,
                    NombreUsuario = user.UserName,
                    CorreoUsuario = user.Email,
                    CargoUsuario = roles.FirstOrDefault() ?? "Sin cargo"
                });
            }

            return Ok(lista);
        }

        // OBTENER USUARIO POR ID
        [HttpGet("Obtener/{id}")]
        public async Task<IActionResult> Obtener(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound("Usuario no encontrado.");

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new
            {
                Id = user.Id,
                NombreUsuario = user.UserName,
                CorreoUsuario = user.Email,
                CargoUsuario = roles.FirstOrDefault() ?? "Sin cargo"
            });
        }

        // ACTUALIZAR USUARIO
        [HttpPut("Actualizar/{id}")]
        public async Task<IActionResult> Actualizar(
            string id,
            [FromBody] UserDTO userDTO)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound("Usuario no encontrado.");

            if (string.IsNullOrWhiteSpace(userDTO.NombreUsuario) ||
                string.IsNullOrWhiteSpace(userDTO.CorreoUsuario))
            {
                return BadRequest("Nombre y correo son obligatorios.");
            }

            if (userDTO.CargoUsuario != "Administrador" &&
                userDTO.CargoUsuario != "Vendedor")
            {
                return BadRequest("El cargo seleccionado no es válido.");
            }

            user.UserName = userDTO.NombreUsuario;
            user.Email = userDTO.CorreoUsuario;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var rolesActuales = await _userManager.GetRolesAsync(user);

            if (rolesActuales.Any())
            {
                var quitarRoles = await _userManager.RemoveFromRolesAsync(
                    user, rolesActuales);

                if (!quitarRoles.Succeeded)
                    return BadRequest(quitarRoles.Errors);
            }

            var agregarRol = await _userManager.AddToRoleAsync(
                user, userDTO.CargoUsuario);

            if (!agregarRol.Succeeded)
                return BadRequest(agregarRol.Errors);

            return Ok("Usuario actualizado correctamente.");
        }

        // ELIMINAR USUARIO
        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound("Usuario no encontrado.");

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Usuario eliminado correctamente.");
        }
    }
}