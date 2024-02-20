using WebServicesBackend.Database;

namespace WebServicesBackend.Services
{
    public class RoomService
    {
        /// <summary>
        /// Method wich is used for adding a room 
        /// </summary>
        /// <param name="roomName">the name of the to be created room </param>
        /// <param name="houseId">the house id the room should be assigned to</param>
        /// <returns>A tuple of a boolean and an integer. the boolean indicates whether the creation was successfull or not and the integer is the id of the newly created room </returns>
        public Tuple<bool, int?> AddRoom(string roomName, int houseId)
        {
            var roomDbService = new DatabaseRoomService();
            var result = roomDbService.AddRoom(roomName, houseId);

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
        public Tuple<bool, string?> UpdateRoom(string newRoomName, int roomId)
        {
            var roomDbService = new DatabaseRoomService();
            var result = roomDbService.UpdateRoom(newRoomName, roomId);

            return (result.Item1) ? result : new Tuple<bool, string?>(false, null);
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
            var result = roomDbService.UpdateRoomTemperature(roomId, newTemperature);

            return (result) ? true : false;
        }
    }
}
