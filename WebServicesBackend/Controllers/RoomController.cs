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

        [Route("/GetRoomById")]
        [HttpPost]
        public IActionResult GetRoomByRoomId(int roomId)
        {
            var roomService = new RoomService();
            var result = roomService.GetRoomById(roomId);
            return (result  != null)? Ok(result) : BadRequest();
            
        }

        [Route("/GetAllRooms")]
        [HttpPost]
        public IActionResult GetAllRooms()
        {
            var roomService = new RoomService();
            var result = roomService.GetAllRooms();
            return (result != null) ? Ok(result) : BadRequest();

        }

    }
}