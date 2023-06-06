using AutoMapper;
using CursoAngular.API.Dto.Users;
using CursoAngular.UOW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CursoAngular.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>/signin
        [HttpPost("signin")]
        public async Task<ActionResult<TokenDto>> Post([FromBody] SignInDto signIn)
        {
            try
            {
                var result = await unitOfWork.UsersRepository.SignInAsync(signIn.Name, signIn.Email, signIn.Password);

                if (result.Succeeded)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim("email", signIn.Email),
                        new Claim("name", signIn.Name)
                    };

                    return GenerateToken(claims);
                }

                return BadRequest(result.Errors);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST api/<UsersController>/login
        [HttpPost("login")]
        public async Task<ActionResult<TokenDto>> Post([FromBody] LogInDto logIn)
        {
            try
            {
                var user = await unitOfWork.UsersRepository.GetUserAsync(logIn.Email);

                if (user is not null)
                {
                    var result = await unitOfWork.UsersRepository.LogInAsync(user, logIn.Password);

                    if (result.Succeeded)
                    {
                        var claims = await unitOfWork.UsersRepository.GetClaimsAsync(user);

                        return GenerateToken(claims);
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

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private TokenDto GenerateToken(IList<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Keys:Jwt"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(30);
            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expires, signingCredentials: credentials);

            return new TokenDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expires
            };
        }
    }
}
