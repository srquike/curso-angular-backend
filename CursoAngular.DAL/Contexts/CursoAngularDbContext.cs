using CursoAngular.BOL;
using CursoAngular.BOL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CursoAngular.DAL;
public class CursoAngularDbContext : IdentityDbContext
{
    public virtual DbSet<CinemaEntity> Cinemas { get; set; }
    public virtual DbSet<MovieEntity> Movies { get; set; }
    public virtual DbSet<GenreEntity> Genres { get; set; }
    public virtual DbSet<StarEntity> Stars { get; set; }
    public virtual DbSet<CastEntity> Cast { get; set; }
    public virtual DbSet<GenreMovieEntity> GenreMovies { get; set; }
    public virtual DbSet<MovieCinemaEntity> MovieCinemas { get; set; }
    public virtual DbSet<RatingEntity> Ratings { get; set; }

    public CursoAngularDbContext(DbContextOptions<CursoAngularDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CinemaEntity>(entity =>
        {
            entity.Property(e => e.Location)
                .HasColumnType("geography");

            entity.HasMany(d => d.MovieCinemas).WithOne(p => p.Cinema)
                .HasForeignKey(p => p.CinemaId)
                .HasConstraintName("FK_Cinemas_MoviesCinemas_CinemaId");
        });

        modelBuilder.Entity<MovieEntity>(entity =>
        {
            entity.HasMany(p => p.Casting).WithOne(d => d.Movie)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK_Movies_StarsMovies_MovieId");

            entity.HasMany(p => p.GenreMovies).WithOne(d => d.Movie)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK_Movies_GenresMovies_MovieId");
            
            entity.HasMany(p => p.CinemaMovies).WithOne(d => d.Movie)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK_Movies_MoviesCinemas_MovieId");
        });

        modelBuilder.Entity<GenreEntity>(entity =>
        {
            entity.HasMany(p => p.GenreMovies).WithOne(d => d.Genre)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK_Genres_GenresMovies_GenreId");
        });

        modelBuilder.Entity<StarEntity>(entity =>
        {
            entity.HasMany(p => p.StarMovies).WithOne(d => d.Star)
                .HasForeignKey(d => d.StarId)
                .HasConstraintName("FK_Stars_StarsMovies_StarId");
        });
    }
}