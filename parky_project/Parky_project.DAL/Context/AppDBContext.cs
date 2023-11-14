using Microsoft.EntityFrameworkCore;
using parky_project.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parky_project.DAL.Context
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
        {
            
        }
        public DbSet<NationalPark> NationalParks { get; set;}
    }
}
