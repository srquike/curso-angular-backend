using System.ComponentModel.DataAnnotations;

namespace CursoAngular.API.Dto.Users
{
    public class LogInDto
    {
        [EmailAddress]
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
