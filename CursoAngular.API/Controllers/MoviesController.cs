using AutoMapper;
using CursoAngular.BOL;
using CursoAngular.BOL.DTOs;
using CursoAngular.UOW;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CursoAngular.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MoviesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MoviesController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // GET: api/<MoviesController>
        [HttpGet]
        public async Task<ActionResult<List<MovieDTO>>> Get()
        {
            try
            {
                var movies = await _unitOfWork.Repository<MovieEntity>().Get();

                if (movies.Count <= 0)
                {
                    return NoContent();
                }

                var result = _mapper.Map<List<MovieDTO>>(movies);

                return result;
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET api/<MoviesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDTO>> Get(int id)
        {
            try
            {
                var movie = await _unitOfWork.Repository<MovieEntity>().GetById(id);

                if (movie != null)
                {
                    var result = _mapper.Map<MovieDTO>(movie);
                    return result;
                }

                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST api/<MoviesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MovieDTO movie)
        {
            try
            {
                var result = _mapper.Map<MovieEntity>(movie);
                _unitOfWork.Repository<MovieEntity>().Create(result);

                if (await _unitOfWork.SaveChangesAsync())
                {
                    return CreatedAtAction(nameof(Get), new { id = result.Id }, new { id = result.Id});
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // PUT api/<MoviesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MovieDTO movie)
        {
            try
            {
                if (await _unitOfWork.Repository<MovieEntity>().Exists(id))
                {
                    var result = _mapper.Map<MovieEntity>(movie);
                    _unitOfWork.Repository<MovieEntity>().Update(result);

                    if (await _unitOfWork.SaveChangesAsync())
                    {
                        return NoContent();
                    }

                    return Conflict();
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // DELETE api/<MoviesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _unitOfWork.Repository<MovieEntity>().Exists(id))
                {
                    _unitOfWork.Repository<MovieEntity>().Delete(id);

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
