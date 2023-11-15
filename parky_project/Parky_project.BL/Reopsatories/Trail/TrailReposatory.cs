using AutoMapper;
using Microsoft.EntityFrameworkCore;
using parky_project.API.Models;
using Parky_project.BL.Dtos.Trail;
using Parky_project.BL.TrailReopsatory;
using Parky_project.DAL.Context;
using Parky_project.DAL.Dtos;
using Parky_project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Parky_project.BL.TrailReopsatory
{

    public class TrailReposatory : ITrailReposatory
    {
        private AppDBContext context;
        private IMapper mapper;
        public TrailReposatory(AppDBContext _context,IMapper _mapper)
        {
            this.context = _context;
            this.mapper = _mapper;
            
        }
        public async Task AddTrail(TrailAddDto TrailDto)
        {
            var insertedTrail = mapper.Map<Trail>(TrailDto);
            
           await context.Trails.AddAsync(insertedTrail);
            await SaveChanges();

           
        }

        public async Task DeleteTrail(int id)
        {
            //

            var DeletedTrail = context.Trails.FirstOrDefault(n=>n.id==id)!;
           // var DeletedTrail2 = mapper.Map<Trail>(DeletedTrail);
            context.Trails.Remove(DeletedTrail);
              await SaveChanges();

            
        }

        public async Task< ICollection<TrailDto>> GetAllTrails()
        {
            var trails = context.Trails.Include(c => c.NationalPark).Where(c => c.NationalPark.id == c.NationalParkId).ToList();
            var TrailsListInDto = new List<TrailDto>();
            foreach (var trailReturned in trails)
            {
                  TrailsListInDto
                    .Add(  mapper.Map<TrailDto>(trailReturned));
            }
            return  TrailsListInDto;

        }

        public  TrailDto? GetTrail(int id)
        {
            var TrailFromDb =  context.Trails.Include(c => c.NationalPark).Where( t => t.NationalPark.id == t.NationalParkId ).FirstOrDefault( n => n.id == id );
            
           

            if (TrailFromDb == null)
            {
                return null;
            }
            else
            {
                
                return mapper.Map<TrailDto>(TrailFromDb);
            }
        }

        public async Task GetTrail(string name)
        {
           await context.Trails.AnyAsync(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            
        }

        public async Task TrailExist(string name)
        {
            await context.Trails.AnyAsync(n=>n.Name == name);
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateTrail(int id,  TrailUpdateDto trailDto)
        {
            var TrailFromDB= await context.Trails.FirstOrDefaultAsync(a => a.id == id);

            if(TrailFromDB != null && TrailFromDB.id == id)
            {
                mapper.Map(trailDto,TrailFromDB);
                 

            }
            else
            {
                throw new Exception("no valid data");
            }
            await SaveChanges();
        }

        public async Task<ICollection<Trail>> GetTrailNationalPark(int NationalParkId)
        {
            return (  context.Trails.Include(c => c.NationalPark).Where(c => c.NationalPark.id == NationalParkId).ToList());
        }


    }
}
