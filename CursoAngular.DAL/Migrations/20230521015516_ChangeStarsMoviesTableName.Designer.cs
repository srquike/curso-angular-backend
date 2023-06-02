﻿// <auto-generated />
using System;
using CursoAngular.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;

#nullable disable

namespace CursoAngular.DAL.Migrations
{
    [DbContext(typeof(CursoAngularDbContext))]
    [Migration("20230521015516_ChangeStarsMoviesTableName")]
    partial class ChangeStarsMoviesTableName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CursoAngular.BOL.CinemaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Point>("Location")
                        .HasColumnType("geography");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Cinemas", (string)null);
                });

            modelBuilder.Entity("CursoAngular.BOL.Entities.CastEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Character")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("StarId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("StarId");

                    b.ToTable("Cast");
                });

            modelBuilder.Entity("CursoAngular.BOL.Entities.GenreMovieEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("MovieId");

                    b.ToTable("GenreMovies");
                });

            modelBuilder.Entity("CursoAngular.BOL.Entities.MovieCinemaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CinemaId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CinemaId");

                    b.HasIndex("MovieId");

                    b.ToTable("MovieCinemas");
                });

            modelBuilder.Entity("CursoAngular.BOL.GenreEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Genres", (string)null);
                });

            modelBuilder.Entity("CursoAngular.BOL.MovieEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MpaaRating")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PosterUrl")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("TrailerUrl")
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movies", (string)null);
                });

            modelBuilder.Entity("CursoAngular.BOL.StarEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PhotographyURL")
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Stars", (string)null);
                });

            modelBuilder.Entity("CursoAngular.BOL.Entities.CastEntity", b =>
                {
                    b.HasOne("CursoAngular.BOL.MovieEntity", "Movie")
                        .WithMany("StarMovies")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Movies_StarsMovies_MovieId");

                    b.HasOne("CursoAngular.BOL.StarEntity", "Star")
                        .WithMany("StarMovies")
                        .HasForeignKey("StarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Stars_StarsMovies_StarId");

                    b.Navigation("Movie");

                    b.Navigation("Star");
                });

            modelBuilder.Entity("CursoAngular.BOL.Entities.GenreMovieEntity", b =>
                {
                    b.HasOne("CursoAngular.BOL.GenreEntity", "Genre")
                        .WithMany("GenreMovies")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Genres_GenresMovies_GenreId");

                    b.HasOne("CursoAngular.BOL.MovieEntity", "Movie")
                        .WithMany("GenreMovies")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Movies_GenresMovies_MovieId");

                    b.Navigation("Genre");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("CursoAngular.BOL.Entities.MovieCinemaEntity", b =>
                {
                    b.HasOne("CursoAngular.BOL.CinemaEntity", "Cinema")
                        .WithMany("MovieCinemas")
                        .HasForeignKey("CinemaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Cinemas_MoviesCinemas_CinemaId");

                    b.HasOne("CursoAngular.BOL.MovieEntity", "Movie")
                        .WithMany("CinemaMovies")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Movies_MoviesCinemas_MovieId");

                    b.Navigation("Cinema");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("CursoAngular.BOL.CinemaEntity", b =>
                {
                    b.Navigation("MovieCinemas");
                });

            modelBuilder.Entity("CursoAngular.BOL.GenreEntity", b =>
                {
                    b.Navigation("GenreMovies");
                });

            modelBuilder.Entity("CursoAngular.BOL.MovieEntity", b =>
                {
                    b.Navigation("CinemaMovies");

                    b.Navigation("GenreMovies");

                    b.Navigation("StarMovies");
                });

            modelBuilder.Entity("CursoAngular.BOL.StarEntity", b =>
                {
                    b.Navigation("StarMovies");
                });
#pragma warning restore 612, 618
        }
    }
}
