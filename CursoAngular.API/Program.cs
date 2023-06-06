using AutoMapper;
using CursoAngular.API.Filters;
using CursoAngular.API.Mapper.Profiles;
using CursoAngular.DAL;
using CursoAngular.DAL.Repositories.Files;
using CursoAngular.DAL.UnitOfWork;
using CursoAngular.Repository.Files;
using CursoAngular.UOW;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System.Text;

namespace CursoAngular.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(corsbuilder =>
                {
                    corsbuilder.WithOrigins(builder.Configuration.GetValue<string>("Clients"))
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders(new string[] { "itemsCount" });
                });
            });

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ ";
                options.User.RequireUniqueEmail = true;
            });

            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<CursoAngularDbContext>().AddDefaultTokenProviders();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Keys:Jwt"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ExceptionLoggerFilter));
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<CursoAngularDbContext>(options =>
            {
                options.EnableSensitiveDataLogging().UseSqlServer("Name=ConnectionStrings:CursoAngularDb", provider =>
                {
                    provider.EnableRetryOnFailure();
                    provider.UseNetTopologySuite();
                });
            });

            builder.Services.AddSingleton(NtsGeometryServices.Instance.CreateGeometryFactory(4326));

            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IFilesStorageRepository, FilesStorageRepository>(_ =>
            {
                return new FilesStorageRepository(builder.Configuration["ConnectionStrings:AzureBlobStorage"]);
            });
            builder.Services.AddSingleton(provider =>

                new MapperConfiguration(configuration =>
                {
                    var geometryFactory = provider.GetRequiredService<GeometryFactory>();
                    configuration.AddProfile(new CinemasProfiles(geometryFactory));
                    configuration.AddProfile(new GenresProfiles());
                    configuration.AddProfile(new StarsProfiles());
                    configuration.AddProfile(new MoviesProfiles());
                }).CreateMapper()
            );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}