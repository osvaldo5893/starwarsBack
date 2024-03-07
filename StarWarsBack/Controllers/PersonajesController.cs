using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarWarsBack.Models;
using System.Net.Http;

namespace StarWarsBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;

        public PersonajesController(ApplicationDbContext context)
        {
            _context = context;
            _httpClient = new HttpClient();

        }
        [HttpGet]
        [Route("getPersonajes")]
        public async Task<IActionResult> getUsers()
        {
            try
            {
                string apiUrl = "https://swapi.dev/api/people";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // Procesar la respuesta del servidor como sea necesario
                    return Ok(responseBody);
                }
                else
                {
                    return BadRequest((int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }






















    }
}
