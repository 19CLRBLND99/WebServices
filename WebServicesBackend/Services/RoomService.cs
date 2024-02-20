using WebServicesBackend.Database;

namespace WebServicesBackend.Services
{
    public class RoomService
    {

       public Tuple<bool, int?> AddRoom(string roomName, int roomId)
        {
            var roomDbService = new DatabaseRoomService();
            var result = roomDbService.AddRoom(roomName, roomId);

            return (result.Item1) ? result : new Tuple<bool, int?>(false, null);
        }

        public bool DeleteRoom(int roomId)
        {
            var roomDbService = new DatabaseRoomService();
            var result = roomDbService.DeleteRoom(roomId);

            return (result) ? result : false;
        }

         public Tuple<bool, string?> UpdateRoom(string newRoomName, int roomId)
        {
            var roomDbService = new DatabaseRoomService();
            var result = roomDbService.UpdateRoom(newRoomName, roomId);

            return (result.Item1) ? result : new Tuple<bool, string?>(false, null);
        }

        public Tuple<bool, double?> UpdateRoomTemperature(int roomId, double newTemperature)
        {
            var roomDbService = new DatabaseRoomService();
            var result = roomDbService.UpdateRoomTemperature(roomId, newTemperature);

            return (result.Item1) ? result : new Tuple<bool, double?>(false, null);
        }
    }
}
