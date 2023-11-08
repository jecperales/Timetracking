using api.timetracking.data.Repositories;
using api.timetracking.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.timetracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly IMovimientoRepository _movimientosRepository;

        public MovimientosController(IMovimientoRepository movimientoRepository)
        {
            _movimientosRepository = movimientoRepository;
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> FindById(int id) 
        {
            return Ok(await _movimientosRepository.FindById(id));
        
        }

        [HttpGet("Hist")]
        public async Task<IActionResult> FindAllByDate(int id_usuario, string f1, string f2, int id_proyecto, int id_perfil)
        {
            return Ok(await _movimientosRepository.FindAllByDate(id_usuario, f1, f2, id_proyecto, id_perfil));
        }

        [HttpPost]
        public async Task<IActionResult> InsertMov([FromBody] movimientos mov) 
        {
            if (mov == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _movimientosRepository.InsertMovimiento(mov);

            return Created("Created", created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMov([FromBody] movimientos mov)
        {
            if (mov == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var res = await _movimientosRepository.UpdateMovimiento(mov);

            //return NoContent();
            return Accepted(res);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletMov(int id)
        {
            var res = await _movimientosRepository.DeleteMovimiento(id);

            //return NoContent();
            return Accepted("Deleted", res);
        }



    }
}
