using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using parky_project.API.Models;
using Parky_project.BL.NationalParkReopsatory;
using Parky_project.DAL.Dtos;

namespace parky_project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalParksController : ControllerBase
    {
        #region injection
        private readonly INationalParkReposatory _nationalParkRepo;
        private readonly Mapper _mapper;
        public NationalParksController(INationalParkReposatory nationalParkRepo,Mapper mapper)
        {
            _mapper = mapper;
            _nationalParkRepo= nationalParkRepo;
            
        }

        #endregion

        [HttpGet]
        public async Task< ActionResult<IEnumerable<NationalParkDto>>> GetAllNationalParks() 
        {
            

            return Ok(await _nationalParkRepo.GetAllNationalParks());

        }

        [HttpGet]
        [Route("{id}")]
        public async Task< ActionResult<NationalParkDto>> GetNationPark(int id)
        {
            return Ok(await _nationalParkRepo.GetNationalPark(id));
            //var nationalParkFromDb = _nationalParkRepo.GetNationalPark(id);
            //if (nationalParkFromDb != null)
            //{
            //    var returendNationalPark = _mapper.Map<NationalParkDto>(nationalParkFromDb);
            //    return Ok(returendNationalPark);

            //}
            //return NotFound();

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

            // var addedNationalPark = _mapper.Map<NationalPark>(nationalParkDto);
            await _nationalParkRepo.AddNationalPark(nationalParkDto);
            return Ok(nationalParkDto);



        }


    }
}
