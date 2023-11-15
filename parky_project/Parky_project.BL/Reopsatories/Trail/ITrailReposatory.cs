using parky_project.API.Models;
using Parky_project.BL.Dtos.Trail;
using Parky_project.DAL.Dtos;
using Parky_project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parky_project.BL.TrailReopsatory
{
    public interface ITrailReposatory
    {
       Task< ICollection<TrailDto>> GetAllTrails();
        Task<ICollection<Trail>> GetTrailNationalPark(int NationalParkId);
         TrailDto? GetTrail(int id);
        Task GetTrail(string name);
        Task AddTrail(TrailAddDto TrailDto);
        Task DeleteTrail(int id);
        Task UpdateTrail(int id, TrailUpdateDto Trail);
        Task TrailExist(string  name);
        Task SaveChanges();
        
    }
}
