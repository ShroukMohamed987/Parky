using AutoMapper;
using Microsoft.EntityFrameworkCore;
using parky_project.API.Models;

using Parky_project.DAL.Context;

using Parky_project.DAL.Dtos;
using Parky_project.DAL.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Parky_project.BL.NationalParkReopsatory
{
    
    public class NationalParkReposatory : INationalParkReposatory
    {
        private AppDBContext context;
        private IMapper mapper;
        public NationalParkReposatory(AppDBContext _context,IMapper _mapper)
        {
            this.context = _context;
            this.mapper = _mapper;
            
        }
        public async Task AddNationalPark(NationalParkDto nationalParkDto)
        {
            var insertedNationalPark = mapper.Map<NationalPark>(nationalParkDto);
            
           await context.NationalParks.AddAsync(insertedNationalPark);
            await SaveChanges();

           
        }

        public async Task DeleteNationalPark(int id)
        {
            

            var DeletedNationalPark = context.NationalParks.FirstOrDefault(n=>n.id==id)!;
            if(DeletedNationalPark == null || DeletedNationalPark.id != id)
            {
                throw new Exception("NO Data Found");
            }

            context.NationalParks.Remove(DeletedNationalPark);
              await SaveChanges();

            
        }

        public async Task< ICollection<NationalParkDto>> GetAllNationalParks(int? PageNumber )
        {
            var nationalParks =  context.NationalParks.ToList();
            var NationalParksListInDto = new List<NationalParkDto>();
            foreach (var nationalParkReturned in nationalParks)
            {
                NationalParksListInDto
                    .Add(mapper.Map<NationalParkDto>(nationalParkReturned));
            }

            //pagination
            int pageSize = 5;
            NationalParksListInDto = Pagination<NationalParkDto>.create(NationalParksListInDto.AsQueryable(), PageNumber ?? 1, pageSize);
            return NationalParksListInDto;

            //return Ok(NationalParksListInDto);

            //return (ICollection<NationalParkDto>)context.NationalParks.ToList();
        }

        public async Task< NationalParkDto?> GetNationalPark(int id)
        {
            var NationalParkFromDb =await context.NationalParks.FirstOrDefaultAsync(n => n.id == id);

            if (NationalParkFromDb == null)
            {
                return null;
            }
            else
            {
                return mapper.Map<NationalParkDto>(NationalParkFromDb);
            }
        }

        public async Task GetNationalPark(string name)
        {
            await context.NationalParks.AnyAsync(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            
        }

        public async Task NationalParkExist(string name)
        {
            await context.NationalParks.AnyAsync(n=>n.Name == name);
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateNationalPark(int id,  NationalParkDto nationalParkDto)
        {
            var NationalParkFromDB= await context.NationalParks.FirstOrDefaultAsync(a => a.id == id);

            if(NationalParkFromDB != null && NationalParkFromDB.id == id)
            {
                mapper.Map(nationalParkDto,NationalParkFromDB);
                 

            }
            else
            {
                throw new Exception("no valid data");
            }
            await SaveChanges();
        }
    }
}
