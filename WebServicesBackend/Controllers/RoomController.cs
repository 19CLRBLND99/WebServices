using Microsoft.AspNetCore.Mvc;
using WebServicesBackend.Services;

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
            var roomService = new RoomService();
            var result = roomService.AddRoom(roomName,houseId);
            return (result.Item1) ? Ok(result.Item2) : BadRequest();
        }

        [Route("/DeleteRoom")]
        [HttpDelete]
        public IActionResult DeleteRoom(int roomId)
        {
            var roomService = new RoomService();
            var result = roomService.DeleteRoom(roomId);
            return (result) ? Ok() : BadRequest();
        }

        [Route("/UpdateRoom")]
        [HttpPost]
        public IActionResult UpdateRoom(string newRoomName,int roomId)
        {
            var roomService = new RoomService();
            var result = roomService.UpdateRoom(newRoomName, roomId);
            return (result.Item1) ? Ok(result.Item2) : BadRequest();
        }

        [Route("/UpdateRoomTemperature")]
        [HttpPost]
        public IActionResult UpdateRoomTemperature(int roomId, double newTemperature)
        {
            var roomService = new RoomService();
            var result = roomService.UpdateRoomTemperature(roomId,newTemperature);
            return (result) ? Ok(newTemperature) : BadRequest();
        }

    }
}