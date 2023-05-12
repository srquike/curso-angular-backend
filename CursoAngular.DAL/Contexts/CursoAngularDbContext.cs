using CursoAngular.BOL;
using CursoAngular.BOL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CursoAngular.DAL;
public class CursoAngularDbContext : DbContext
{
    public virtual DbSet<CinemaEntity> Cinemas { get; set; }
    public virtual DbSet<MovieEntity> Movies { get; set; }
    public virtual DbSet<GenreEntity> Genres { get; set; }
    public virtual DbSet<StarEntity> Stars { get; set; }

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

            //entity.HasMany(p => p.MovieCinemas).WithOne(b => b.Cinema)
            //    .;

            entity.HasMany(c => c.Movies)
                .WithMany(m => m.Cinemas)
                .UsingEntity<MovieCinemaEntity>(
                    "MoviesCinemas",
                    l => l.HasOne<MovieEntity>().WithMany().HasForeignKey(e => e.MovieId),
                    r => r.HasOne<CinemaEntity>().WithMany().HasForeignKey(e => e.CinemaId));
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

            entity.HasMany(m => m.Cast)
                .WithMany(s => s.Movies)
                .UsingEntity<StarMovieEntity>(
                    "StarsMovies",
                    l => l.HasOne<StarEntity>().WithMany().HasForeignKey(e => e.StarId),
                    r => r.HasOne<MovieEntity>().WithMany().HasForeignKey(e => e.MovieId));

            entity.HasMany(m => m.Genres)
                .WithMany(g => g.Movies)
                .UsingEntity<GenreMovieEntity>(
                    "GenresMovies",
                    l => l.HasOne<GenreEntity>().WithMany().HasForeignKey(e => e.GenreId),
                    r => r.HasOne<MovieEntity>().WithMany().HasForeignKey(e => e.MovieId));
        });

        modelBuilder.Entity<GenreEntity>(entity =>
        {
            entity.ToTable("Genres");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .HasColumnType("varchar(50)");
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
        });
    }
}