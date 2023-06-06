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
            entity.ToTable("Cinemas");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .HasColumnType("varchar(50)");
            entity.Property(e => e.Location)
                .HasColumnType("geography");

            entity.HasMany(d => d.MovieCinemas).WithOne(p => p.Cinema)
                .HasForeignKey(p => p.CinemaId)
                .HasConstraintName("FK_Cinemas_MoviesCinemas_CinemaId");

            //entity.HasMany(c => c.Movies)
            //    .WithMany(m => m.Cinemas)
            //    .UsingEntity<MovieCinemaEntity>(
            //        "MoviesCinemas",
            //        l => l.HasOne<MovieEntity>().WithMany().HasForeignKey(e => e.MovieId),
            //        r => r.HasOne<CinemaEntity>().WithMany().HasForeignKey(e => e.CinemaId));
        });

        modelBuilder.Entity<MovieEntity>(entity =>
        {
            entity.ToTable("Movies");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Title)
                .HasColumnType("varchar(100)");
            entity.Property(e => e.ReleaseDate);
            entity.Property(e => e.PosterUrl)
                .HasColumnType("varchar(max)");
            entity.Property(e => e.TrailerUrl)
                .HasColumnType("varchar(max)");
            entity.Property(e => e.MpaaRating)
                .HasColumnType("varchar(50)");

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
            entity.ToTable("Genres");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .HasColumnType("varchar(50)");

            entity.HasMany(p => p.GenreMovies).WithOne(d => d.Genre)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK_Genres_GenresMovies_GenreId");
        });

        modelBuilder.Entity<StarEntity>(entity =>
        {
            entity.ToTable("Stars");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .HasColumnType("varchar(50)");
            entity.Property(e => e.DateOfBirth);
            entity.Property(e => e.PhotographyURL)
                .HasColumnType("varchar(max)");

            entity.HasMany(p => p.StarMovies).WithOne(d => d.Star)
                .HasForeignKey(d => d.StarId)
                .HasConstraintName("FK_Stars_StarsMovies_StarId");
        });
    }
}