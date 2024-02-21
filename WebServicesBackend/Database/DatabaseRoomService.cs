using MySql.Data.MySqlClient;
using WebServicesBackend.Models;

namespace WebServicesBackend.Database
{
    public class DatabaseRoomService
    {
        string connectionString = "Server=172.30.224.1;Database=SmartHomeDB;User ID=root;Password=password;";

        public Tuple<bool, int?> AddRoom(string roomName)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            int result = -1;

            try
            {
                connection.Open();

                Console.WriteLine("Successfully connected to DB");

                string sqlStatement = $"INSERT INTO rooms VALUES(NULL,\"{roomName}\", '21.3',NULL); SELECT last_insert_id();"; // 21.3 is the default room temperature when a new room is created! 

                MySqlCommand command = new MySqlCommand(sqlStatement, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = reader.GetInt32(0);
                        Console.WriteLine(result);
                    }
                }
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error while connecting to DB: {ex.Message}");
                return new Tuple<bool, int?>(false, 0);
            }
            return new Tuple<bool, int?>(true, result);
        }

        public bool DeleteRoom(int roomId)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            bool result;

            try
            {
                connection.Open();
                Console.WriteLine("Successfully connected to DB");

                string sqlStatement = $"DELETE FROM rooms WHERE id = {roomId};";

                using (MySqlCommand command = new MySqlCommand(sqlStatement, connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();

                    result = (rowsAffected == 1) ? true : false;
                }

                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error while connecting to DB: {ex.Message}");
                return false;
            }
            return result;
        }

        public Tuple<bool, string?> UpdateRoomName(int roomId, string newRoomName)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            bool result;

            try
            {
                connection.Open();

                Console.WriteLine("Successfully connected to DB");

                string sqlStatement = $"UPDATE rooms SET name = \"{newRoomName}\" WHERE id = {roomId};";

                using (MySqlCommand command = new MySqlCommand(sqlStatement, connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();

                    result = (rowsAffected == 1) ? true : false;
                }
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error while connecting to DB: {ex.Message}");
                return new Tuple<bool, string?>(false, null);
            }
            return new Tuple<bool, string?>(true, newRoomName);
        }

        public bool AssignThermostatToRoom(int roomId, int thermostatId)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            bool result;

            try
            {
                connection.Open();

                Console.WriteLine("Successfully connected to DB");
                // TODO check if thermostat already is assigned!!! 
                string sqlStatement = $"UPDATE rooms SET thermostatID = '{thermostatId}' WHERE id = {roomId};";

                using (MySqlCommand command = new MySqlCommand(sqlStatement, connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();

                    result = (rowsAffected == 1) ? true : false;
                }
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error while connecting to DB: {ex.Message}");
                return false;
            }
            return result;
        }

        public Tuple<bool,int?> GetThermostatIdByRoomId(int roomId)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            bool result;
            int thermostatId = -1;

            try
            {
                connection.Open();

                Console.WriteLine("Successfully connected to DB");

                string sqlStatement = $"SELECT thermostatID FROM rooms WHERE id = {roomId}; ";

                using (MySqlCommand command = new MySqlCommand(sqlStatement, connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    result = (rowsAffected == 1) ? true : false;
                
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        thermostatId = reader.GetInt32(0);
                    }
                }
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error while connecting to DB: {ex.Message}");
                return new Tuple<bool,int?>(false,null);
            }
            return new Tuple<bool,int?>(result,thermostatId);
        }

        public RoomModel GetRoomById(int roomId)
        {
            RoomModel room = new RoomModel();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT id, name, temperature, thermostatID FROM rooms WHERE id = @RoomId";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@RoomId", roomId);

                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    room = new RoomModel
                    {
                        RoomId = Convert.ToInt32(reader["id"]),
                        RoomName = Convert.ToString(reader["name"]),
                    };
                    if (!reader.IsDBNull(3))
                    {
                        room.ThermostatId = Convert.ToInt32(reader["thermostatID"]);
                    }
                    else {
                        room.ThermostatId = -1;
                    }
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
                    };
                    if (!reader.IsDBNull(3))
                    {
                        room.ThermostatId = Convert.ToInt32(reader["thermostatID"]);
                    }
                    else
                    {
                        room.ThermostatId = -1;
                    }
                    rooms.Add(room);
                }

                reader.Close();
            }

            return rooms;
        }

    }
}
