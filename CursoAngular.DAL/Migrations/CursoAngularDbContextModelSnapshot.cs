﻿// <auto-generated />
using System;
using CursoAngular.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CursoAngular.DAL.Migrations
{
    [DbContext(typeof(CursoAngularDbContext))]
    partial class CursoAngularDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Location")
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Cinemas", (string)null);
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

            modelBuilder.Entity("GenresMovies", b =>
                {
                    b.Property<int>("GenresId")
                        .HasColumnType("int");

                    b.Property<int>("MoviesId")
                        .HasColumnType("int");

                    b.HasKey("GenresId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("GenresMovies");
                });

            modelBuilder.Entity("MoviesCinemas", b =>
                {
                    b.Property<int>("CinemasId")
                        .HasColumnType("int");

                    b.Property<int>("MoviesId")
                        .HasColumnType("int");

                    b.HasKey("CinemasId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("MoviesCinemas");
                });

            modelBuilder.Entity("StarsMovies", b =>
                {
                    b.Property<int>("CastId")
                        .HasColumnType("int");

                    b.Property<int>("MoviesId")
                        .HasColumnType("int");

                    b.HasKey("CastId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("StarsMovies");
                });

            modelBuilder.Entity("GenresMovies", b =>
                {
                    b.HasOne("CursoAngular.BOL.GenreEntity", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CursoAngular.BOL.MovieEntity", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MoviesCinemas", b =>
                {
                    b.HasOne("CursoAngular.BOL.CinemaEntity", null)
                        .WithMany()
                        .HasForeignKey("CinemasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CursoAngular.BOL.MovieEntity", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StarsMovies", b =>
                {
                    b.HasOne("CursoAngular.BOL.StarEntity", null)
                        .WithMany()
                        .HasForeignKey("CastId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CursoAngular.BOL.MovieEntity", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
