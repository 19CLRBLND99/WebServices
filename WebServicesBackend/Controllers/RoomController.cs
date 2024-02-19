using Microsoft.AspNetCore.Mvc;

namespace WebServicesBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        [Route("/AddRoom")]
        [HttpPost]
        public IActionResult AddRoom(string roomName, int houseId)
        {
            return Ok(roomName);
        }

        [Route("/DeleteRoom")]
        [HttpDelete]
        public IActionResult DeleteRoom(int houseId)
        {
            return Ok();
        }

        [Route("/UpdateRoom")]
        [HttpPost]
        public IActionResult UpdateRoom(string newHouseName)
        {
            return Ok(newHouseName);
        }

        [Route("/UpdateRoomTemperature")]
        [HttpPost]
        public IActionResult UpdateRoomTemperature(int roomId, double newTemperature)
        {
            return Ok();
        }

    }
}