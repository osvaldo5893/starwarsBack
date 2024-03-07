using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarWarsBack.Models;
using System;
using System.Security.Cryptography;
using System.Text;

namespace StarWarsBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("getUsers")]
        public async Task<ActionResult<IEnumerable<Usuario>>> getUsers()
        {
            try
            {
                var datos = _context.usuario.FromSqlRaw("select * from usuario;").ToList();
                if (datos.Count() > 0)
                {
                    return Ok(datos);

                }
                else
                {
                    var d = new List<string>()
                    {
                        "SIN INFO"
                    };
                    return Ok(d);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("GuardarUser")]
        public dynamic GuardarUser(Usuario Cuenta)
        {

            try
            {
                Cuenta.password = EncryptPassword(Cuenta.password);
                Cuenta.createdAt = DateTime.Now;
                _context.Add(Cuenta);
                _context.SaveChanges();
                return Ok(Cuenta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("ActualizacionUser")]
        [HttpPut]
        public dynamic ActualizacionUser(int id, [FromBody] Usuario Cuenta)
        {
            try
            {
                if (id == Cuenta.Id)
                {

                    Cuenta.updatedAt = DateTime.Now;
                    _context.Update(Cuenta);
                    _context.SaveChanges();
                    return Ok(Cuenta);

                }
                else
                    return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("EliminarUser")]
        [HttpDelete]
        public dynamic EliminarUser(int id, [FromBody] Usuario Cuenta)
        {
            try
            {
                if (id == Cuenta.Id)
                {
                    _context.Remove(Cuenta);
                    _context.SaveChanges();
                    return Ok(Cuenta);
                }
                else
                    return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public dynamic login(string email, string pass)
        {

            try
            {
                var user = _context.usuario.FirstOrDefault(u => u.email == email);
                if (user != null)
                {
                    if (user.password != null)
                    {
                        // Compara las contraseñas encriptadas
                        if (VerifyPassword(pass, user.password))
                        {
                            return Ok(user);
                        }
                    }
                }
                return BadRequest("Usuario o contraseña Incorrectos.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // Método para encriptar la contraseña
        private string EncryptPassword(string pass)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pass));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            string hashedEnteredPassword = EncryptPassword(enteredPassword);
            return hashedEnteredPassword == storedPassword;
        }























    }
}
