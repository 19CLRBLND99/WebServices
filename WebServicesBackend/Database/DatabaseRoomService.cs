using MySql.Data.MySqlClient;
using WebServicesBackend.Models;

namespace WebServicesBackend.Database
{
    public class DatabaseRoomService
    {
        string connectionString = "Server=172.30.224.1;Database=SmartHomeDB;User ID=root;Password=password;";

        public Tuple<bool, int?> AddRoom(string roomName)
        {
            return new Tuple<bool, int?>(true, 1);
        }

        public bool DeleteRoom(int roomId)
        {
            return true;
        }

        public Tuple<bool, string?> UpdateRoom(string newRoomName, int roomId)
        {
            return new Tuple<bool, string?>(true, "");
        }

        public bool UpdateRoomTemperature(int roomId, double newTemperature)
        {
            return true;
        }

        public RoomModel GetRoomById(int roomId)
        {
            RoomModel room = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT id, name, temperature, thermostatID FROM rooms WHERE id = @RoomId";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@RoomId", roomId);

                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    room = new RoomModel
                    {
                        RoomId = Convert.ToInt32(reader["id"]),
                        RoomName = Convert.ToString(reader["name"]),
                        RoomTemperature = Convert.ToDouble(reader["temperature"]),
                        ThermostatId = Convert.ToInt32(reader["thermostatID"])
                    };
                }

                reader.Close();
            }

            return room;
        }

        public List<RoomModel> GetAllRooms()
        {
            List<RoomModel> rooms = new List<RoomModel>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT id, name, temperature, thermostatID FROM rooms";

                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    RoomModel room = new RoomModel
                    {
                        RoomId = Convert.ToInt32(reader["id"]),
                        RoomName = Convert.ToString(reader["name"]),
                        RoomTemperature = Convert.ToDouble(reader["temperature"]),
                        ThermostatId = Convert.ToInt32(reader["thermostatID"])
                    };
                    rooms.Add(room);
                }

                reader.Close();
            }

            return rooms;
        }

    }
}
