using AutoMapper;
using CursoAngular.API.DTO.Stars;
using CursoAngular.API.Extensions;
using CursoAngular.API.Tools;
using CursoAngular.BOL;
using CursoAngular.Repository.Files;
using CursoAngular.UOW;
using Microsoft.AspNetCore.Mvc;

namespace CursoAngular.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFilesStorageRepository _filesStorage;
        private readonly IMapper _mapper;

        public StarsController(IUnitOfWork unitOfWork, IMapper mapper, IFilesStorageRepository filesStorage)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _filesStorage = filesStorage;
        }

        // GET: api/<StarsController>
        [HttpGet]
        public async Task<ActionResult<List<IndexStarDTO>>> Get([FromQuery] Pagination pagination)
        {
            try
            {
                var stars = await _unitOfWork.Repository<StarEntity>().Get(pagination.GetSkipCount(), pagination.ItemsToDisplay, e => e.Name);

                if (stars.Count <= 0)
                {
                    return NoContent();
                }

                var results = _mapper.Map<List<IndexStarDTO>>(stars);
                var itemsCount = await _unitOfWork.Repository<StarEntity>().GetCount();

                HttpContext.SetPaginationParameters(itemsCount);

                return results;
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET api/<StarsController>/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<StarDTO>> Get(int id)
        {
            try
            {
                var star = await _unitOfWork.Repository<StarEntity>().GetById(id);

                if (star == null)
                {
                    return NotFound();
                }

                var result = _mapper.Map<StarDTO>(star);

                return result;
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST api/<StarsController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromForm] FormStarDTO star)
        {
            try
            {
                var result = _mapper.Map<StarEntity>(star);

                if (star.PhotographyFile != null)
                {
                    result.PhotographyURL = await _filesStorage.UploadFileAsync("stars", star.PhotographyFile.OpenReadStream(), star.PhotographyFile.FileName);
                }

                _unitOfWork.Repository<StarEntity>().Create(result);

                if (await _unitOfWork.SaveChangesAsync())
                {
                    return CreatedAtAction(nameof(Get), new { id = result.Id }, new { createdStarId = result.Id });
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT api/<StarsController>/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromForm] FormStarDTO starDTO)
        {
            try
            {
                var star = await _unitOfWork.Repository<StarEntity>().GetById(id);

                if (star == null)
                {
                    return NotFound();
                }

                var result = _mapper.Map(starDTO, star);
                result.Id = id;

                if (starDTO.PhotographyFile != null)
                {
                    result.PhotographyURL = await _filesStorage.UpdateFileAsync(star.PhotographyURL, "stars", starDTO.PhotographyFile.OpenReadStream(), starDTO.PhotographyFile.FileName);
                }

                _unitOfWork.Repository<StarEntity>().Update(result);

                if (await _unitOfWork.SaveChangesAsync())
                {
                    return NoContent();
                }

                return Conflict();

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // DELETE api/<StarsController>/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _unitOfWork.Repository<StarEntity>().Exists(id))
                {
                    _unitOfWork.Repository<StarEntity>().Delete(id);

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
