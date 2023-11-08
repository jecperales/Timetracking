using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.timetracking.data;
using api.timetracking.models;
using api.timetracking.data.Repositories;
using Microsoft.AspNetCore.Http.Metadata;

namespace api.timetracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TorresController : ControllerBase
    {
        private readonly ITorresRepository _torresRepository;

        public TorresController(ITorresRepository torresRepository)
        {
            _torresRepository = torresRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTorres()
        {
            return Ok(await _torresRepository.GetAllTorres());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            return Ok(await _torresRepository.FindById(id));
        }


    }
}
