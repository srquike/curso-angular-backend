using System.ComponentModel.DataAnnotations;

namespace CursoAngular.API.DTO.Cinemas
{
    public class FormCinemasDTO
    {
        [Required]
        public string? Name { get; set; }

        [Range(-90, 90)]
        public double Latitude { get; set; }

        [Range(-180, 180)]
        public double Longitude { get; set; }
    }
}
