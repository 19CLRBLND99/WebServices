using MySql.Data.MySqlClient;

namespace WebServicesBackend.Database
{
    public class DatabaseThermostatService
    {
        string connectionString = "Server=192.168.2.102;Database=SmartHomeDB;User ID=root;Password=password;";
        public Tuple<bool, int?> AddThermostat()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            int result = -1;

            try
            {
                connection.Open();
                Console.WriteLine("Successfully connected to DB");

                string sqlStatement = "INSERT INTO thermostat VALUES(NULL,NULL); SELECT last_insert_id();";

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

            Console.WriteLine("Added new Thermostat with Id: " + result);
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
    }
}
