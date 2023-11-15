
using Microsoft.EntityFrameworkCore;
using Parky_project.BL.NationalParkReopsatory;
using Parky_project.DAL.Context;


using Parky_project.BL.Mapping;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parky_project.BL.TrailReopsatory;
using Microsoft.AspNetCore.Identity;
using System;
using Parky_project.DAL.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

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
            builder.Services.AddScoped<ITrailReposatory, TrailReposatory>();

            #endregion

            #region mapper

            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            #endregion

            #region cors

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("parky_project", p => p.AllowAnyHeader()
                .AllowAnyOrigin()
                .WithOrigins("http://localhost:7127")
                .WithMethods("PUT", "POST", "DELETE", "GET"));
            });
            #endregion

            #region versioning

            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1.0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;  

            }).AddMvc();

            #endregion

            #region identity
            builder.Services.AddIdentity<User, IdentityRole>(
             option =>
             {
               option.Password.RequireNonAlphanumeric = false;
               option.Password.RequiredLength = 5;
               option.Password.RequireUppercase = true;
               option.Password.RequireLowercase = true;
               option.User.RequireUniqueEmail = true;


           }).AddEntityFrameworkStores<AppDBContext>();
            #endregion

           
            #region Authentication
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "col";
                option.DefaultChallengeScheme = "col";
            }).AddJwtBearer("col", options =>
            {
                var key = builder.Configuration.GetValue<string>("secretKey");
                //conver key to byte
                var keyAsByte = Encoding.ASCII.GetBytes(key ?? string.Empty);
                //convert key byte to object
                var keyObject = new SymmetricSecurityKey(keyAsByte);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = keyObject,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                };
            });
            #endregion

            #region authorization

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>policy
                                                 .RequireClaim(ClaimTypes.Role, "Admin", "Manager")
                                                 .RequireClaim(ClaimTypes.NameIdentifier));
            });

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