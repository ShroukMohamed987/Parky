using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using parky_project.API.Models;
using Parky_project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parky_project.DAL.Context
{
    public class AppDBContext:IdentityDbContext<User>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
        {
            
        }
        public DbSet<NationalPark> NationalParks { get; set;}
        public DbSet<Trail> Trails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Id as an identity column
            modelBuilder.Entity<NationalPark>()
                .Property(n => n.id)
                .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }

        
    }
}
