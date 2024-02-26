using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System.Reflection.PortableExecutable;

namespace WebServicesBackend.Database
{
    public class DatabaseThermostatService
    {
        string connectionString = "Server=mysql-thermostat;Port=3307;Database=thermostat_db;User ID=root;Password=password;";
        public Tuple<bool, int?> AddThermostat(int? thermostatId)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            var result = false;

            try
            {
                connection.Open();
                Console.WriteLine("Successfully connected to DB");

                string sqlStatement = $"INSERT INTO thermostat VALUES({thermostatId},NULL);";

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
                return new Tuple<bool, int?>(result,-1);
            }

            Console.WriteLine("Added new Thermostat with Id: " + thermostatId);
            return new Tuple<bool, int?>(result, thermostatId);
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

        public List<int> GetAllThermostatIds()
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
                return new List<int>();          
            }

            return thermostatIds;
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
                        thermostatIds.Add(reader.GetInt32(0));
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
    
    
        public bool SetThermostatTemperature(int thermostatId, double newTemperature)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            bool result = false;

            try
            {
                connection.Open();
                Console.WriteLine("Successfully connected to DB");

                string sqlStatement = $"UPDATE thermostat SET temperature = \"{newTemperature}\" WHERE id = {thermostatId};";

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

            return true;
        }
    }
}
