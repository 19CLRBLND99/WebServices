using Microsoft.AspNetCore.Mvc;
using WebServicesBackend.Services;

namespace WebServicesBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HouseController : ControllerBase
    {
        [Route("/AddHouse")]
        [HttpPost]
        public IActionResult AddHouse(string houseName)
        {
            var houseService = new HouseService();
            var result = houseService.AddHouse(houseName);
            return (result.Item1) ? Ok(result.Item2) : BadRequest();
        }

        [Route("/DeleteHouse")]
        [HttpDelete]
        public IActionResult DeleteHouse(int houseId)
        {
            var houseService = new HouseService();
            var result = houseService.DeleteHouse(houseId);
            return (result) ? Ok(result) : BadRequest();
        }

        [Route("/UpdateHouse")]
        [HttpPut]
        public IActionResult UpdateHouse(string newHouseName, int houseId)
        {
            var houseService = new HouseService();
            var result = houseService.UpdateHouse(newHouseName,houseId);
            return (result.Item1) ? Ok(result.Item2) : BadRequest();
        }

        [Route("/GetAllHouses")]
        [HttpGet]
        public IActionResult GetAllHouses()
        {
            var houseService = new HouseService();
            var result = houseService.GetAllHouses();
            return (result) ? Ok(result) : BadRequest();
        }

        [Route("/GetHouseById")]
        [HttpGet]
        public IActionResult GetHouseById( int houseId)
        {
            var houseService = new HouseService();
            var result = houseService.GetHouseById(houseId);
            return (result) ? Ok(result) : BadRequest();
        }
    }
}