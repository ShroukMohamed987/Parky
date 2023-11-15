using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ParkyWeb.Models;
using ParkyWeb.Reposatories.IReposatory;

namespace ParkyWeb.Controllers
{
    public class NationalParkController : Controller
    {
        private readonly INationalParkRepo _nationalParkRepo;
        public NationalParkController(INationalParkRepo nationalParkRepo)
        {
            _nationalParkRepo = nationalParkRepo;
        }
        public IActionResult Index()
        {
            return View(new NationalPark());
        }

        public async Task< IActionResult> GetAllNationalPark()
        {
            return  Json(new { data = await _nationalParkRepo.GetAllAsync(StaticData.ApiNationalParkUrl) });
        }
    }
}
