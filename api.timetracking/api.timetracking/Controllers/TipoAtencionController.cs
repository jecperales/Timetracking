using api.timetracking.data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.timetracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoAtencionController : ControllerBase
    {
        private readonly ITipoAtencionRepository _tipoAtencionRepository;

        public TipoAtencionController(ITipoAtencionRepository tipoAtencionRepository)
        {
            _tipoAtencionRepository = tipoAtencionRepository;       
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTipoAtencion()
        {
            return Ok(await _tipoAtencionRepository.GetAllTipoAtencion());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            return Ok(await _tipoAtencionRepository.FindById(id));
        }
    }
}
