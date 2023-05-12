using AutoMapper;
using CursoAngular.API.DTO.Cinemas;
using CursoAngular.API.Extensions;
using CursoAngular.API.Tools;
using CursoAngular.BOL;
using CursoAngular.UOW;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CursoAngular.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CinemasController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/<CinemasController>
        [HttpGet]
        public async Task<ActionResult<List<IndexCinemasDTO>>> Get([FromQuery] Pagination pagination)
        {
            try
            {
                var cinemas = await _unitOfWork.Repository<CinemaEntity>().Get(pagination.GetSkipCount(), pagination.ItemsToDisplay, e => e.Name);

                if (cinemas.Count <= 0)
                {
                    return NoContent();
                }

                var results = _mapper.Map<List<IndexCinemasDTO>>(cinemas);
                var itemsCount = await _unitOfWork.Repository<CinemaEntity>().GetCount();

                HttpContext.SetPaginationParameters(itemsCount);

                return results;
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET api/<CinemasController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CinemasController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormCinemasDTO cinema)
        {
            try
            {
                var result = _mapper.Map<CinemaEntity>(cinema);

                _unitOfWork.Repository<CinemaEntity>().Create(result);

                if (await _unitOfWork.SaveChangesAsync())
                {
                    return CreatedAtAction(nameof(Get), new { id = result.Id }, new { createdCinemaId = result.Id });
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT api/<CinemasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CinemasController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _unitOfWork.Repository<CinemaEntity>().Exists(id))
                {
                    _unitOfWork.Repository<CinemaEntity>().Delete(id);

                    if (await _unitOfWork.SaveChangesAsync())
                    {
                        return NoContent();
                    }

                    return Conflict();
                }

                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
