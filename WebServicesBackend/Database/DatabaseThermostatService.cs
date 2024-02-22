using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System.Reflection.PortableExecutable;

namespace WebServicesBackend.Database
{
    public class DatabaseThermostatService
    {
        string connectionString = "Server=192.168.2.102;Database=SmartHomeDB;User ID=root;Password=password;";
        public Tuple<bool, int?> AddThermostat(int? thermostatId)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            int result = -1;

            try
            {
                connection.Open();
                Console.WriteLine("Successfully connected to DB");

                string sqlStatement = $"INSERT INTO thermostat VALUES({thermostatId},NULL);";

                MySqlCommand command = new MySqlCommand(sqlStatement, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = reader.GetInt32(0);
                    }
                }
                connection.Close();
            }

            catch (MySqlException ex)
            {
                Console.WriteLine($"Error while connecting to DB: {ex.Message}");
                return new Tuple<bool, int?>(false, 0);
            }

            Console.WriteLine("Added new Thermostat with Id: " + thermostatId);
            return new Tuple<bool, int?>(true, result);
        }

        public bool DeleteThermostat(int thermostatId)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            bool result;

            try
            {
                connection.Open();
                Console.WriteLine("Successfully connected to DB");

                string sqlStatement = $"DELETE FROM thermostat WHERE id = {thermostatId};";

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

            Console.WriteLine("Deleted Thermostat with Id: " + thermostatId);
            return result;
        }

        public List<int>? GetAllThermostatIds()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            List<int> thermostatIds = new List<int>();

            try
            {
                connection.Open();
                Console.WriteLine("Successfully connected to DB");

                string sqlStatement = "SELECT id FROM thermostat";

                MySqlCommand command = new MySqlCommand(sqlStatement, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        thermostatIds.Add(reader.GetInt32(0));
                    }
                }
                connection.Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error while connecting to DB: {ex.Message}");
                return null;          
            }

            return thermostatIds;
        }
    }
}
