using MySql.Data.MySqlClient;

namespace WebServicesBackend.Database
{
    public class DatabaseThermostatService
    {
        string connectionString = "Server=172.18.96.1;Database=SmartHomeDB;User ID=root;Password=password;";
        public Tuple<bool,int?> AddThermostat()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("Verbindung erfolgreich geöffnet!");

                string sqlStatement = "SELECT * FROM rooms"; 

                MySqlCommand command = new MySqlCommand(sqlStatement, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine();
                    }
                }
                connection.Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error while connecting to DB: {ex.Message}");
            }
        }

        public bool DeleteThermostat(int thermostatId)
        {
            //TODO implement deleting stuff and after deleting thermostat return true 
            return true;
        }
    }
}
