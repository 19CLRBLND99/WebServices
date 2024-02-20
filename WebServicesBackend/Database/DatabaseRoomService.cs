namespace WebServicesBackend.Database
{
    public class DatabaseRoomService
    {
        public Tuple<bool, int?> AddRoom(string roomName, int roomId)
        {

            return new Tuple<bool, int?>(true, 1);
        }

        public bool DeleteRoom(int roomId)
        {
            return true;
        }

        public Tuple<bool, string?> UpdateRoom(string newRoomName, int roomId)
        {
            return new Tuple<bool, string?>(true, "Barbies neues Schminkzimmer");
        }

        public bool UpdateRoomTemperature(int roomId, double newTemperature)
        {
            return true;
        }
    }
}
