using AutoMapper;
using CursoAngular.BOL;
using CursoAngular.UOW;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CursoAngular.Repository.Files;
using CursoAngular.API.DTO.Movies;
using CursoAngular.API.DTO.Cinemas;
using CursoAngular.API.DTO;
using CursoAngular.API.Extensions;


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
        private readonly IFilesStorageRepository _filesStorage;
        private const string MOVIES_CONTAINER_NAME = "movies";

        public MoviesController(IMapper mapper, IUnitOfWork unitOfWork, IFilesStorageRepository filesStorage)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _filesStorage = filesStorage;
        }

        // GET: api/<MoviesController>
        [HttpGet]
        public async Task<ActionResult<List<MovieDTO>>> Get([FromQuery] FilterDTO filter)
        {
            var results = new List<MovieDTO>();
            var query = _unitOfWork.Repository<MovieEntity>();

            if (!string.IsNullOrEmpty(filter.Title))
            {
                query.Filter(x => x.Title.Contains(filter.Title));
            }

            if (filter.ComingSoon)
            {
                query.Filter(x => x.ReleaseDate > DateTime.Today);
            }
            else
            {
                query.Filter(x => x.ReleaseDate <= DateTime.Today);
            }

            if (filter.Genre is not 0)
            {
                query.Filter(x => x.GenreMovies.Select(y => y.GenreId).Contains(filter.Genre));
            }

            query.Skip(filter.Pagination.GetSkipCount()).Take(filter.Pagination.ItemsToDisplay);

            var movies = await query.Get();
            var itemsCount = await _unitOfWork.Repository<MovieEntity>().GetCount();

            results = _mapper.Map<List<MovieDTO>>(movies);

            HttpContext.SetPaginationParameters(itemsCount);

            return results;

        }

        [HttpGet("landing")]
        public async Task<ActionResult<LandingPageDTO>> Get()
        {
            var top = 5;
            var today = DateTime.Today;

            var comingSoon = await _unitOfWork.Repository<MovieEntity>().Filter(x => x.ReleaseDate > today).Order(x => x.ReleaseDate).Take(top).Get();
            var onCinemas = await _unitOfWork.Repository<MovieEntity>().Filter(x => x.ReleaseDate <= today).Order(x => x.ReleaseDate).Take(top).Get();

            var result = new LandingPageDTO()
            {
                ComingSoon = _mapper.Map<List<MovieDTO>>(comingSoon),
                OnCinemas = _mapper.Map<List<MovieDTO>>(onCinemas)
            };

            return result;
        }

        // GET api/<MoviesController>/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDTO>> Get(int id)
        {
            try
            {
                var movie = await _unitOfWork.MoviesRespository.Get(id, asNoTracking: true);

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

        [HttpGet("edit/{id:int}")]
        public async Task<ActionResult<EditMovieDTO>> GetForEditing(int id)
        {
            var actionResult = await Get(id);

            if (actionResult.Result is NotFoundResult)
            {
                return NotFound();
            }

            var movie = actionResult.Value;

            var genres = movie.Genres.Select(g => g.Id).ToList();
            var noSelectedGenres = await _unitOfWork.Repository<GenreEntity>().Filter(x => !genres.Contains(x.Id)).Get();

            var cinemas = movie.Cinemas.Select(c => c.Id).ToList();
            var noSelectedCinemas = await _unitOfWork.Repository<CinemaEntity>().Filter(x => !cinemas.Contains(x.Id)).Get();

            var result = new EditMovieDTO()
            {
                Cast = movie.Cast,
                Cinemas = movie.Cinemas,
                Genres = movie.Genres,
                Movie = movie,
                NoSelectedCinemas = _mapper.Map<List<CinemaDTO>>(noSelectedCinemas),
                NoSelectedGenres = _mapper.Map<List<GenreDTO>>(noSelectedGenres)
            };

            return result;
        }

        [HttpGet("resources")]
        public async Task<ActionResult<MovieResourcesDTO>> GetResources()
        {
            var cinemas = await _unitOfWork.Repository<CinemaEntity>().Order(x => x.Name).Get();
            var genres = await _unitOfWork.Repository<GenreEntity>().Order(x => x.Name).Get();

            var result = new MovieResourcesDTO()
            {
                Cinemas = _mapper.Map<List<IndexCinemasDTO>>(cinemas),
                Genres = _mapper.Map<List<GenreDTO>>(genres)
            };

            return result;
        }

        // POST api/<MoviesController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromForm] FormMovieDTO movie)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = _mapper.Map<MovieEntity>(movie);

                if (movie.PosterFile != null)
                {
                    result.PosterUrl = await _filesStorage.UploadFileAsync(MOVIES_CONTAINER_NAME, movie.PosterFile.OpenReadStream(), movie.PosterFile.FileName);
                }

                _unitOfWork.Repository<MovieEntity>().Create(result);

                if (await _unitOfWork.SaveChangesAsync())
                {
                    return CreatedAtAction(nameof(Get), new { id = result.Id }, new { id = result.Id });
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT api/<MoviesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] FormMovieDTO movieDto)
        {
            try
            {
                var movie = await _unitOfWork.MoviesRespository.Get(id, asNoTracking: false);

                if (movie is null)
                {
                    return NotFound();
                }

                var result = _mapper.Map(movieDto, movie);
                result.Id = id;

                if (movieDto.PosterFile is not null)
                {
                    result.PosterUrl = await _filesStorage.UpdateFileAsync(movie.PosterUrl, MOVIES_CONTAINER_NAME, movieDto.PosterFile.OpenReadStream(), movieDto.PosterFile.FileName);
                }

                _unitOfWork.Repository<MovieEntity>().Update(result);

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

        // DELETE api/<MoviesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _unitOfWork.Repository<MovieEntity>().Exists(id))
                {
                    var movie = await _unitOfWork.Repository<MovieEntity>().Get(id);

                    _unitOfWork.Repository<MovieEntity>().Delete(id);

                    if (await _unitOfWork.SaveChangesAsync())
                    {
                        await _filesStorage.DeleteFileAsync(movie.PosterUrl, MOVIES_CONTAINER_NAME);
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
