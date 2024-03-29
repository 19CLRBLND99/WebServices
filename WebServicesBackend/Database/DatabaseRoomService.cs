﻿using MySql.Data.MySqlClient;
using WebServicesBackend.HelperFunctions;
using WebServicesBackend.Models;

namespace WebServicesBackend.Database
{
    public class DatabaseRoomService
    {
        string connectionString = "Server=mysql-rooms;Port=3306;Database=rooms_db;User ID=root;Password=password;";

        public Tuple<bool, int?> AddRoom(string roomName, int? roomId)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            var result = false;

            try
            {
                connection.Open();

                Console.WriteLine("Successfully connected to DB");

                string sqlStatement = $"INSERT INTO rooms VALUES({roomId},\"{roomName}\", 0);";

                using (MySqlCommand command = new MySqlCommand(sqlStatement, connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();

                    result = (rowsAffected == 1) ? true : false;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error while connecting to DB: {ex.Message}");
                return new Tuple<bool, int?>(result, -1);
            }

            Console.WriteLine("Added new Room with Id: " + roomId);
            return new Tuple<bool, int?>(result, roomId);
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

            Console.WriteLine("Deleted Room with Id: " + roomId);
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

            Console.WriteLine("Updated Name of the room with Id '" + roomId + "' to new Name: \"" + newRoomName + "\"");
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

            Console.WriteLine("Assigned Thermostat with Id '" + thermostatId + "' to the Room with Id '" + roomId + "'");
            return result;
        }

        public Tuple<bool, int?> GetThermostatIdByRoomId(int roomId)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            int thermostatId = -1;

            try
            {
                connection.Open();

                Console.WriteLine("Successfully connected to DB");

                string sqlStatement = $"SELECT thermostatID FROM rooms WHERE id = {roomId}; ";

                using (MySqlCommand command = new MySqlCommand(sqlStatement, connection))
                {

                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        thermostatId = HelperFunctionsClass.SafeGetInt(reader, 0);
                    }
                }
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error while connecting to DB: {ex.Message}");
                return new Tuple<bool, int?>(false, null);
            }

            Console.WriteLine("Returned thermostatId(" + thermostatId + ") for room with Id '" + roomId + "'");
            return new Tuple<bool, int?>(true, thermostatId);
        }

        public RoomModel GetRoomById(int roomId)
        {
            RoomModel room = new RoomModel();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT id, name, thermostatID FROM rooms WHERE id = @RoomId";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@RoomId", roomId);

                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    room = new RoomModel
                    {
                        RoomId = HelperFunctionsClass.SafeGetInt(reader, 0),
                        RoomName = HelperFunctionsClass.SafeGetString(reader, 1),
                    };
                    if (!reader.IsDBNull(2))
                    {
                        room.ThermostatId = Convert.ToInt32(reader["thermostatID"]);
                    }
                    else
                    {
                        room.ThermostatId = -1;
                    }
                }

                reader.Close();
            }

            Console.WriteLine("Returned data for room with Id '" + roomId + "'");
            return room;
        }

        public List<RoomModel> GetAllRooms()
        {
            List<RoomModel> rooms = new List<RoomModel>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT id, name, thermostatID FROM rooms";

                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    RoomModel room = new RoomModel
                    {
                        RoomId = HelperFunctionsClass.SafeGetInt(reader, 0),
                        RoomName = HelperFunctionsClass.SafeGetString(reader, 1),
                    };
                    if (!reader.IsDBNull(2))
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
            Console.WriteLine("Returned data for all rooms");
            return rooms;
        }

        public List<int> GetAllRoomIds()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            List<int> roomIds = new List<int>();

            try
            {
                connection.Open();
                Console.WriteLine("Successfully connected to DB");

                string sqlStatement = "SELECT id FROM rooms";

                MySqlCommand command = new MySqlCommand(sqlStatement, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roomIds.Add(HelperFunctionsClass.SafeGetInt(reader, 0));
                    }
                }
                connection.Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error while connecting to DB: {ex.Message}");
                return new List<int>();
            }

            return roomIds;
        }

        public List<int> GetAllAssignedThermostatIds()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            List<int> thermostatIds = new List<int>();

            try
            {
                connection.Open();
                Console.WriteLine("Successfully connected to DB");

                string sqlStatement = "SELECT thermostatID FROM rooms";

                MySqlCommand command = new MySqlCommand(sqlStatement, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        thermostatIds.Add(HelperFunctionsClass.SafeGetInt(reader, 0));
                    }
                }
                connection.Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error while connecting to DB: {ex.Message}");
                return new List<int>();
            }

            return thermostatIds;
        }
    }
}
