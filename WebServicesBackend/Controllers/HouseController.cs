using Microsoft.AspNetCore.Mvc;

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
            return Ok(houseName);
        }

        [Route("/DeleteHouse")]
        [HttpDelete]
        public IActionResult DeleteHouse(int houseId)
        {
            return Ok();
        }

        [Route("/UpdateHouse")]
        [HttpPut]
        public IActionResult UpdateHouse(string newHouseName)
        {
            return Ok(newHouseName);
        }
    }
}