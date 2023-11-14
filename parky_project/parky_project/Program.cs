
using Microsoft.EntityFrameworkCore;
using Parky_project.BL.NationalParkReopsatory;
using Parky_project.DAL.Context;


using Parky_project.BL.Mapping;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace parky_project.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region default
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #endregion

            #region database
            var connectionString = builder.Configuration.GetConnectionString("Parky");

            builder.Services.AddDbContext<AppDBContext>(options =>
            {
                options.UseSqlServer( connectionString ) ;
            });
            #endregion

            #region services
            builder.Services.AddScoped<INationalParkReposatory, NationalParkReposatory>();
            #endregion

            #region mapper
            
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            #endregion

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