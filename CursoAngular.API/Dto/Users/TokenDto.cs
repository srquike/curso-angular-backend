namespace CursoAngular.API.Dto.Users
{
    public class TokenDto
    {
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
