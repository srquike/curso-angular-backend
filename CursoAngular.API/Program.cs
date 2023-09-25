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
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CursoAngular.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(corsbuilder =>
                {
                    corsbuilder.WithOrigins("http://localhost:4200")
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

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"])),
                    ClockSkew = TimeSpan.Zero
                }
            );

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("admin", policy => policy.RequireClaim("role", "admin"));
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
                options.EnableSensitiveDataLogging().UseNpgsql(builder.Configuration.GetConnectionString("CursoAngularDb"), provider =>
                {
                    provider.EnableRetryOnFailure();
                    provider.UseNetTopologySuite();
                });
            });

            builder.Services.AddSingleton(NtsGeometryServices.Instance.CreateGeometryFactory(4326));

            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IFilesStorageRepository, LocalFileStorageRepository>();
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

            app.UseStaticFiles();

            app.MapControllers();

            using (var scope = app.Services.CreateAsyncScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<CursoAngularDbContext>();
                db.Database.Migrate();

                var userManager = (UserManager<IdentityUser>)scope.ServiceProvider.GetService(typeof(UserManager<IdentityUser>));
                var roleManager = (RoleManager<IdentityRole>)scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>));

                await DataInitializer.SeedData(userManager, roleManager);
            }

            app.Run();
        }
    }

    internal class DataInitializer
    {
        internal static async Task SeedData(UserManager<IdentityUser>? userManager, RoleManager<IdentityRole>? roleManager)
        {
            await SeedRole(roleManager);
            await SeedUser(userManager);
        }

        private static async Task SeedUser(UserManager<IdentityUser>? userManager)
        {
            var findedUser = await userManager.FindByNameAsync("Administrator");

            if (findedUser is null)
            {
                var newUser = new IdentityUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "Administrator",
                    Email = "administrator@starflix.com"
                };

                var result = await userManager.CreateAsync(newUser, "Administrator1!");

                if (result.Succeeded)
                {
                    var roleResult = await userManager.AddToRoleAsync(newUser, "Administrator");

                    if (roleResult.Succeeded)
                    {
                        var claims = new List<Claim>()
                        {
                            new Claim("email", newUser.Email),
                            new Claim("name", newUser.UserName),
                            new Claim("role", "Administrator")
                        };

                        await userManager.AddClaimsAsync(newUser, claims);
                    }
                }
            }
        }

        private static async Task SeedRole(RoleManager<IdentityRole>? roleManager)
        {
            var findedRole = await roleManager.FindByNameAsync("Administrator");

            if (findedRole is null)
            {
                var newRole = new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                };

                await roleManager.CreateAsync(newRole);
            }
        }
    }
}