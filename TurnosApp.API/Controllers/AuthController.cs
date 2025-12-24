using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurnosApp.Infrastructure.Data;

namespace TurnosApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TurnosDbContext _context;

        public AuthController(TurnosDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var doctor = await _context.Doctores
                .FirstOrDefaultAsync(d => d.Usuario.ToLower() == login.Usuario.ToLower() && d.Password == login.Password);

            if (doctor == null)
            {
                return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos" });
            }

            return Ok(new { 
                id = doctor.Id, 
                nombre = doctor.Nombre,
                mensaje = "Bienvenido al sistema WaitLess" 
            });
        }
    }

    public class LoginRequest
    {
        public string Usuario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}