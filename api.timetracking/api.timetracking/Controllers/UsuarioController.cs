using api.timetracking.data.Repositories;
using api.timetracking.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.timetracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;


        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _usuarioRepository.GetAllUsuarios());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            return Ok(await _usuarioRepository.FindById(id));
        }

        [HttpGet("Auth")]
        public async Task<IActionResult> Auth(string user, string password)
        {
            return Ok(await _usuarioRepository.Auth(user, password));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] Usuario usuario) 
        {
            if (usuario == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _usuarioRepository.InsertUsuario(usuario);

            return Created("Created", created);
        
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _usuarioRepository.UpdateUsuario(usuario);

            return NoContent();

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUsuario(Usuario usuario)
        {
            await _usuarioRepository.DeleteUsuario(new Usuario { id = usuario.id });

            return NoContent();

        }


    }
}
