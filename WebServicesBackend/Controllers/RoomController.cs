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
        public IActionResult AddRoom(string roomName)
        {
            var roomService = new RoomService();
            var result = roomService.AddRoom(roomName);
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

        [Route("/UpdateRoomName")]
        [HttpPost]
        public IActionResult UpdateRoomName(int roomId, string newRoomName)
        {
            var roomService = new RoomService();
            var result = roomService.UpdateRoomName(roomId, newRoomName);
            return (result.Item1) ? Ok(result.Item2) : BadRequest();
        }

        [Route("/UpdateRoomTemperature")]
        [HttpPost]
        public IActionResult UpdateRoomTemperature(int roomId, double newTemperature)
        {
            var roomService = new RoomService();
            var result = roomService.UpdateRoomTemperature(roomId, newTemperature);
            return (result) ? Ok(newTemperature) : BadRequest();
        }

        [Route("/AssignThermostatToRoom")]
        [HttpPost]
        public IActionResult AssignThermostatToRoom(int roomId, int thermostatId)
        {
            var roomService = new RoomService();
            var result = roomService.AssignThermostatToRoom(roomId, thermostatId);
            return (result) ? Ok() : BadRequest();
        }

        [Route("/GetRoomById")]
        [HttpGet]
        public IActionResult GetRoomByRoomId(int roomId)
        {
            var roomService = new RoomService();
            var result = roomService.GetRoomById(roomId);
            return (result != null) ? Ok(result) : BadRequest();
        }

        [Route("/GetAllRooms")]
        [HttpGet]
        public IActionResult GetAllRooms()
        {
            var roomService = new RoomService();
            var result = roomService.GetAllRooms();
            return (result != null) ? Ok(result) : BadRequest();
        }

    }
}