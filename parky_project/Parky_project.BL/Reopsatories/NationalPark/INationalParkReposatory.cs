using parky_project.API.Models;
using Parky_project.DAL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parky_project.BL.NationalParkReopsatory
{
    public interface INationalParkReposatory
    {
       Task< ICollection<NationalParkDto>> GetAllNationalParks();
        Task< NationalParkDto?> GetNationalPark(int id);
        Task GetNationalPark(string name);
        Task AddNationalPark(NationalParkDto nationalPark);
        Task DeleteNationalPark(NationalParkDto nationalPark);
        Task UpdateNationalPark(int id, NationalParkDto nationalPark);
        Task NationalParkExist(string  name);
        Task SaveChanges();
        
    }
}
