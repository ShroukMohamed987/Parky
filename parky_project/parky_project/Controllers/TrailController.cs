using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using parky_project.API.Models;
using Parky_project.BL.Dtos.Trail;
using Parky_project.BL.TrailReopsatory;

using Parky_project.DAL.Dtos;

namespace parky_project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrailController : Controller
    {
        #region injection
        private readonly ITrailReposatory _trailRepo;
        private readonly IMapper _mapper;
        public TrailController(ITrailReposatory trailReposatory,IMapper mapper)
        {
            _mapper = mapper;
            _trailRepo= trailReposatory;
            
        }

        #endregion

        [HttpGet]
        public async Task< ActionResult<IEnumerable<TrailDto>>> GetAllTrails() 
        {
            

            return Ok(await _trailRepo.GetAllTrails());

        }


        [HttpGet]
        [Route("{id}",Name = "GetTrail")]
        public  ActionResult<TrailDto> GetTrail(int id)
        {

            return Ok( _trailRepo.GetTrail(id));

        }

        [HttpPost]
        public async Task< ActionResult> AddTrail(TrailAddDto trailDto)
        {
            if (trailDto == null)
            {
                return BadRequest(ModelState);
            }
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _trailRepo.AddTrail(trailDto);

            return Ok(trailDto);
       
        }


        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult> UpdateTrail(int id ,TrailUpdateDto trailDto)
        {
            await _trailRepo.UpdateTrail(id,trailDto);
            
            return NoContent();
        }



        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeLeteTrail(int id)
        {
            await _trailRepo.DeleteTrail(id);
            return NoContent();
        }

        [HttpGet]
        [Route("[action]/{NationalParkId}")]

        public async Task<ActionResult<ICollection<TrailAddDto>>> GetTrailInNationalParks(int NationalParkId)
        {
            var TrailListInNationalPark = await _trailRepo.GetTrailNationalPark(NationalParkId);
            
            if (TrailListInNationalPark == null)
            {
                return NotFound();
            }
            
            var ReturnedList = new List<TrailAddDto>();

            foreach(var trail in TrailListInNationalPark)
            {
                ReturnedList.Add( _mapper.Map<TrailAddDto>(trail));
            }
            return Ok(ReturnedList);
        }


    }
}
