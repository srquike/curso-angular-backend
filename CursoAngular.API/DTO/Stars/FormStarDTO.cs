namespace CursoAngular.API.DTO.Stars
{
    public class FormStarDTO
    {
        public string? Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public IFormFile? PhotographyFile { get; set; }
    }
}
