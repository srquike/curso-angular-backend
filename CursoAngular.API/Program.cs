
using CursoAngular.DAL;
using CursoAngular.DAL.UnitOfWork;
using CursoAngular.UOW;
using Microsoft.EntityFrameworkCore;

namespace CursoAngular.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<CursoAngularDbContext>(options => options
                .EnableSensitiveDataLogging()
                .UseSqlServer("Name=ConnectionStrings:CursoAngularDb", provider => provider.EnableRetryOnFailure()));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}