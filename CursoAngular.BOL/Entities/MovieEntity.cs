﻿using CursoAngular.BOL.Entities;

namespace CursoAngular.BOL;
public class MovieEntity : BaseEntity
{
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? PosterUrl { get; set; }
    public string? TrailerUrl { get; set; }
    public string? MpaaRating { get; set; }

    public virtual ICollection<CastEntity> Casting { get; set; } = new List<CastEntity>();
    public virtual ICollection<GenreMovieEntity> GenreMovies { get; set; } = new List<GenreMovieEntity>();
    public virtual ICollection<MovieCinemaEntity> CinemaMovies { get; set; } = new List<MovieCinemaEntity>();
}
