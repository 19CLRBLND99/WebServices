using Microsoft.AspNetCore.Mvc;
using WebServicesBackend.Services;

namespace WebServicesBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        /// <summary>
        /// Controller Endpoint for adding a new room
        /// </summary>
        /// <param name="roomName">the room name of the room to add</param>
        /// <returns>
        /// IActionResult Ok(int) - successfully added room
        /// IActionResult BadRequest() - problem while adding room
        /// </returns>
        [Route("/AddRoom")]
        [HttpPost]
        public IActionResult AddRoom(string roomName)
        {
            var roomService = new RoomService();
            var result = roomService.AddRoom(roomName);
            return (result.Item1) ? Ok(result.Item2) : BadRequest();
        }

        /// <summary>
        /// Controller endpoint for deleting an existing room
        /// </summary>
        /// <param name="roomId">room id of the room to be deleted</param>
        /// <returns>
        /// IActionResult Ok() - successfully deleted room
        /// IActionResult BadRequest() - problem while deleting room
        /// </returns>
        [Route("/DeleteRoom")]
        [HttpDelete]
        public IActionResult DeleteRoom(int roomId)
        {
            var roomService = new RoomService();
            var result = roomService.DeleteRoom(roomId);
            return (result) ? Ok() : BadRequest();
        }

        /// <summary>
        /// Controller Endpoint for updating the Room name of an existing room
        /// </summary>
        /// <param name="roomId">the roomId of the room to be updated</param>
        /// <param name="newRoomName">the new room name</param>
        /// <returns>
        /// IActionResult Ok(string) - successfully updated room name
        /// IActionResult BadRequest() - problem while updating room name
        /// </returns>
        [Route("/UpdateRoomName")]
        [HttpPost]
        public IActionResult UpdateRoomName(int roomId, string newRoomName)
        {
            var roomService = new RoomService();
            var result = roomService.UpdateRoomName(roomId, newRoomName);
            return (result.Item1) ? Ok(result.Item2) : BadRequest();
        }

        /// <summary>
        /// A Controller Endpoint for updating the room temperature 
        /// </summary>
        /// <param name="roomId">the room id of the room to be updated</param>
        /// <param name="newTemperature">the new temperature</param>
        /// <returns>
        /// IActionResult Ok(double) - successfully updated room temperature 
        /// IActionResult BadRequest() - problem while updating room temperature
        /// </returns>
        [Route("/UpdateRoomTemperature")]
        [HttpPost]
        public IActionResult UpdateRoomTemperature(int roomId, double newTemperature)
        {
            var roomService = new RoomService();
            var result = roomService.UpdateRoomTemperature(roomId, newTemperature);
            return (result) ? Ok(newTemperature) : BadRequest();
        }

        /// <summary>
        /// Controller Endpoint for assigning a thermostat to a room 
        /// </summary>
        /// <param name="roomId">the roomId of the room the thermostat should be assigned to</param>
        /// <param name="thermostatId">the thermostatId that should be assigned to the room</param>
        /// <returns>
        /// IActionResult Ok() - successfully assigned thermostat to room
        /// IActionResult BadRequest() - problem while assigning thermostat to room
        /// </returns>
        [Route("/AssignThermostatToRoom")]
        [HttpPost]
        public IActionResult AssignThermostatToRoom(int roomId, int thermostatId)
        {
            var roomService = new RoomService();
            var result = roomService.AssignThermostatToRoom(roomId, thermostatId);
            return (result) ? Ok() : BadRequest();
        }

        /// <summary>
        /// Controller endpoint for getting a single room by its id
        /// </summary>
        /// <param name="roomId">the id of the room to get </param>
        /// <returns>
        /// IActionResult Ok(RoomModel) - successfully fetched room by its id
        /// IActionResult BadRequest() - problem while fetching room by its id
        /// </returns>
        [Route("/GetRoomById")]
        [HttpGet]
        public IActionResult GetRoomByRoomId(int roomId)
        {
            var roomService = new RoomService();
            var result = roomService.GetRoomById(roomId);
            return (result != null) ? Ok(result) : BadRequest();
        }

        /// <summary>
        /// Controller endpoint for getting a single roomModel with its assigned thermostat model 
        /// </summary>
        /// <param name="roomId">the id of the room to get</param>
        /// <returns>
        /// IActionResult Ok(RoomWithThermostatModel) - successfully fetched the room and thermostat
        /// IActionResult BadRequest() - problem while fetching room with thermostat
        /// </returns>
        [Route("/GetRoomWithThermostatByRoomId")]
        [HttpGet]
        public IActionResult GetRoomWithThermostatByRoomId(int roomId)
        {
            var roomService = new RoomService();
            var result = roomService.GetRoomWithThermostatByRoomId(roomId);
            return (result != null) ? Ok(result) : BadRequest();
        }

        /// <summary>
        /// Controller endpoint for getting all available rooms 
        /// </summary>
        /// <returns>
        /// IActionResult Ok(List<RoomModel>) - successfully fetched all rooms
        /// IActionResult BadRequest() - problem while fetching all rooms
        /// </returns>
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