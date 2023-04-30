using CursoAngular.BOL;
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

            entity.HasKey(e => e.CinemaId);

            entity.Property(e => e.Name)
                .HasColumnType("varchar(50)");
            entity.Property(e => e.Location)
                .HasColumnType("varchar(MAX)");

            entity.HasMany(c => c.Movies)
                .WithMany(m => m.Cinemas)
                .UsingEntity("MoviesCinemas");
        });

        modelBuilder.Entity<MovieEntity>(entity =>
        {
            entity.ToTable("Movies");

            entity.HasKey(e => e.MovieId);

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
                .UsingEntity("StarsMovies");
            entity.HasMany(m => m.Genres)
                .WithMany(g => g.Movies)
                .UsingEntity("GenresMovies");
        });

        modelBuilder.Entity<GenreEntity>(entity =>
        {
            entity.ToTable("Genres");

            entity.HasKey(e => e.GenreId);

            entity.Property(e => e.Name)
                .HasColumnType("varchar(50)");
        });

        modelBuilder.Entity<StarEntity>(entity =>
        {
            entity.ToTable("Stars");

            entity.HasKey(e => e.StarId);

            entity.Property(e => e.Name)
                .HasColumnType("varchar(50)");
            entity.Property(e => e.DateOfBirth);
            entity.Property(e => e.PhotographyURL)
                .HasColumnType("varchar(max)");
        });
    }
}