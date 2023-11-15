using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using parky_project.API.Models;
using Parky_project.BL.NationalParkReopsatory;
using Parky_project.DAL.Dtos;

namespace parky_project.API.Controllers
{
    [ApiVersion("1.0")]
    // [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}[controller]")]
    [ApiController]
    public class NationalParksController : Controller
    {
        #region injection
        private readonly INationalParkReposatory _nationalParkRepo;
        private readonly IMapper _mapper;
        public NationalParksController(INationalParkReposatory nationalParkRepo,IMapper mapper)
        {
            _mapper = mapper;
            _nationalParkRepo= nationalParkRepo;
            
        }

        #endregion

        [HttpGet]
        [Route("All/{pageNumber}")]
        public async Task< ActionResult<IEnumerable<NationalParkDto>>> GetAllNationalParks(int pageNumber) 
        {
            

            return Ok(await _nationalParkRepo.GetAllNationalParks(pageNumber));

        }

        [HttpGet]
        [Route("{id}",Name = "GetNationPark")]
        public async Task< ActionResult<NationalParkDto>> GetNationPark(int id)
        {
            return Ok(await _nationalParkRepo.GetNationalPark(id));
            

        }

        [HttpPost]
        public async Task< ActionResult> AddNationalPark(NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null)
            {
                return BadRequest(ModelState);
            }
            //var checkExist= await _nationalParkRepo.NationalParkExist(nationalParkDto.Name)
            //if ()
            //{
            //    ModelState.AddModelError("", "national park exist");
            //    return BadRequest(ModelState);
            //}
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _nationalParkRepo.AddNationalPark(nationalParkDto);
            
            return Ok(nationalParkDto);

            // if i return created at route but i use versioning 

            //return CreatedAtRoute(Version=HttpContext.GetRequestedApiVersion().ToString())
       
        }
        [Authorize(Policy ="Admin")]
        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult> UpdateNationalPark(int id ,NationalParkDto nationalParkDto)
        {
            await _nationalParkRepo.UpdateNationalPark(id,nationalParkDto);
            
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeLeteNationalPark(int id)
        {
            
            
            await _nationalParkRepo.DeleteNationalPark(id);
            return NoContent();
        }


    }
}
