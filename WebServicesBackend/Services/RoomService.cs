using WebServicesBackend.Database;
using WebServicesBackend.HelperFunctions;
using WebServicesBackend.Models;

namespace WebServicesBackend.Services
{
    public class RoomService
    {
        /// <summary>
        /// Method wich is used for adding a room 
        /// </summary>
        /// <param name="roomName">the name of the to be created room </param>
        /// <returns>A tuple of a boolean and an integer. the boolean indicates whether the creation was successfull or not and the integer is the id of the newly created room </returns>
        public Tuple<bool, int?> AddRoom(string roomName)
        {
            var roomId = HelperFunctionsClass.GetNextFreeRoomId();
            if (roomId > 25)
            {
                return new Tuple<bool, int?>(false, null);
            }

            var roomDbService = new DatabaseRoomService();
            var result = roomDbService.AddRoom(roomName, roomId);

            return (result.Item1) ? result : new Tuple<bool, int?>(false, null);
        }

        /// <summary>
        /// Method which is used for deleting an existing room
        /// </summary>
        /// <param name="roomId">The Id of the room to be deleted </param>
        /// <returns>A boolean that indicates whether the deletion was successfull or not </returns>
        public bool DeleteRoom(int roomId)
        {
            var roomDbService = new DatabaseRoomService();
            var result = roomDbService.DeleteRoom(roomId);

            return (result) ? result : false;
        }

        /// <summary>
        /// Method which is used for updating the name of a room 
        /// </summary>
        /// <param name="newRoomName">the new name for the room </param>
        /// <param name="roomId">the id of an existing room </param>
        /// <returns>a tuple of a boolean and a string. The boolean indicates whether the update was successfull or not and the string is the updated name </returns>
        public Tuple<bool, string?> UpdateRoomName(int roomId, string newRoomName)
        {
            var roomDbService = new DatabaseRoomService();
            var result = roomDbService.UpdateRoomName(roomId, newRoomName);

            return (result.Item1) ? result : new Tuple<bool, string?>(false, null);
        }

        /// <summary>
        /// Method which is used for assigning a thermometer to a specific room 
        /// </summary>
        /// <param name="roomId">the room id </param>
        /// <param name="thermostatId">the thermometer id</param>
        /// <returns>a boolean which inidactes whether the assigning process was successfull or not </returns>
        public bool AssignThermostatToRoom(int roomId, int thermostatId)
        {
            if (roomId > 25 || thermostatId > 25)
            {
                return false;
            }
            var result = false;
            var roomDbService = new DatabaseRoomService();
            var thermostatDbService = new DatabaseThermostatService();

            var alreadyAssignedThermostatIds = roomDbService.GetAllAssignedThermostatIds();
            var allAvailablethermostatIds = thermostatDbService.GetAllThermostatIds();

            if (alreadyAssignedThermostatIds.Contains(thermostatId))
            {
                return false;
            }
            if (allAvailablethermostatIds.Contains(thermostatId))
            {
                result = roomDbService.AssignThermostatToRoom(roomId, thermostatId);
            }

            return (result) ? true : false;
        }

        /// <summary>
        /// method which is used for updating the room temperature 
        /// </summary>
        /// <param name="roomId">the id of the to be updated room </param>
        /// <param name="newTemperature"> the new temperature for the room</param>
        /// <returns>a boolean which indicates whether updating the temperature was successfull or not</returns>
        public bool UpdateRoomTemperature(int roomId, double newTemperature)
        {
            var roomDbService = new DatabaseRoomService();
            var thermostatService = new ThermostatService();
            var result = roomDbService.GetThermostatIdByRoomId(roomId);

            var thermostatId = result.Item2;

            thermostatService.SetThermostatTemperature(thermostatId, newTemperature);

            return (result.Item1) ? true : false;
        }


        /// <summary>
        /// method which is used for getting a room via its id
        /// </summary>
        /// <param name="roomId">the id of the to be returned room </param>
        /// <returns>the roomModel</returns>
        public RoomModel GetRoomById(int roomId)
        {
            var roomDbService = new DatabaseRoomService();
            var result = roomDbService.GetRoomById(roomId);

            return result;
        }

        /// <summary>
        /// method which is used for getting a room via its id
        /// </summary>
        /// <param name="roomId">the id of the to be returned room </param>
        /// <returns>the roomModel</returns>
        public RoomWithThermostatModel GetRoomWithThermostatByRoomId(int roomId)
        {
            var result = new RoomWithThermostatModel();

            var roomDbService = new DatabaseRoomService();
            var thermostatDbService = new DatabaseThermostatService();

            var room = roomDbService.GetRoomById(roomId);
            var thermostat = thermostatDbService.GetThermostatById(room.ThermostatId);

            result.RoomName = room.RoomName;
            result.RoomId =room.RoomId;
            result.ThermostatId = room.ThermostatId;
            if (thermostat != null)
            {
                result.Thermostat = thermostat;
            }


            return result;
        }

        /// <summary>
        /// method which is used for getting all rooms
        /// </summary>
        /// <returns>a list of roomModels</returns>
        public List<RoomModel> GetAllRooms()
        {
            var roomDbService = new DatabaseRoomService();
            var result = roomDbService.GetAllRooms();

            return result;
        }
    }
}
