using AutoMapper;
using CursoAngular.API.DTO;
using CursoAngular.API.Extensions;
using CursoAngular.API.Tools;
using CursoAngular.BOL;
using CursoAngular.UOW;
using Microsoft.AspNetCore.Mvc;

namespace CursoAngular.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GenresController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // GET: api/<GenresController>
        [HttpGet]
        public async Task<ActionResult<List<GenreDTO>>> Get([FromQuery] Pagination pagination)
        {
            try
            {
                var genres = await _unitOfWork.Repository<GenreEntity>().Order(x => x.Name).Skip(pagination.GetSkipCount()).Take(pagination.ItemsToDisplay).Get();

                if (genres.Count <= 0)
                {
                    return NoContent();
                }

                var itemsCount = await _unitOfWork.Repository<GenreEntity>().GetCount();
                var results = _mapper.Map<List<GenreDTO>>(genres);

                HttpContext.SetPaginationParameters(itemsCount);

                return results;
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET api/<GenresController>/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GenreDTO>> Get(int id)
        {
            try
            {
                var genre = await _unitOfWork.Repository<GenreEntity>().Get(id);

                if (genre == null)
                {
                    return NotFound();
                }

                var result = _mapper.Map<GenreDTO>(genre);

                return result;
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST api/<GenresController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] GenreDTO genre)
        {
            try
            {
                var result = _mapper.Map<GenreEntity>(genre);

                _unitOfWork.Repository<GenreEntity>().Create(result);

                if (await _unitOfWork.SaveChangesAsync())
                {
                    return CreatedAtAction(nameof(Get), new { id = result.Id }, new { createdGenreId = result.Id });
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT api/<GenresController>/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] GenreDTO genre)
        {
            try
            {
                if (await _unitOfWork.Repository<GenreEntity>().Exists(id))
                {
                    var result = _mapper.Map<GenreEntity>(genre);
                    result.Id = id;

                    _unitOfWork.Repository<GenreEntity>().Update(result);

                    if (await _unitOfWork.SaveChangesAsync())
                    {
                        return NoContent();
                    }

                    return BadRequest();
                }

                return NotFound();

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // DELETE api/<GenresController>/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _unitOfWork.Repository<GenreEntity>().Exists(id))
                {
                    _unitOfWork.Repository<GenreEntity>().Delete(id);

                    if (await _unitOfWork.SaveChangesAsync())
                    {
                        return NoContent();
                    }

                    return BadRequest();
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
