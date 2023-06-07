using AutoMapper;
using CursoAngular.API.Dto.Ratings;
using CursoAngular.BOL.Entities;
using CursoAngular.UOW;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CursoAngular.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RatingsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RatingsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // GET: api/<RatingsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RatingsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RatingsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RatingFormDto dto)
        {
            try
            {
                var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("email")).Value;

                if (userEmailClaim is null)
                {
                    return NotFound("El correo electrónico no fue encontrado");
                }

                var user = await unitOfWork.UsersRepository.GetUserAsync(userEmailClaim);

                if (user is null)
                {
                    return NotFound("El usuario no fue encontrado");
                }

                var rating = await unitOfWork.RatingsRepository.GetAsync(dto.MovieId, user.Id);

                if (rating is not null)
                {
                    rating.Scoring = dto.Scoring;

                    unitOfWork.Repository<RatingEntity>().Update(rating);
                }
                else
                {
                    var result = new RatingEntity() { MovieId = dto.MovieId, UserId = user.Id, Scoring = dto.Scoring };

                    unitOfWork.Repository<RatingEntity>().Create(result);                    
                }

                if (await unitOfWork.SaveChangesAsync())
                {
                    return NoContent();
                }

                return Conflict();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<RatingsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RatingsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
