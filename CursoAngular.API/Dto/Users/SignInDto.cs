using System.ComponentModel.DataAnnotations;

namespace CursoAngular.API.Dto.Users
{
    public class SignInDto
    {
        [Required]
        public string? Name { get; set; }

        [EmailAddress]
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
