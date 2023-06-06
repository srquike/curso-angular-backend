using CursoAngular.API.Tools;

namespace CursoAngular.API.DTO.Movies
{
    public class FilterDTO
    {
        public int PageNumber { get; set; }
        public int ItemsToDisplay { get; set; }
        public Pagination Pagination { get { return new Pagination() { PageNumber = PageNumber, ItemsToDisplay = ItemsToDisplay }; } }
        public string? Title { get; set; }
        public int Genre { get; set; }
        public bool ComingSoon { get; set; }
    }
}
