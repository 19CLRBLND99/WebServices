using MySql.Data.MySqlClient;
using WebServicesBackend.Database;

namespace WebServicesBackend.HelperFunctions
{
    public static class HelperFunctionsClass
    {
        public static int? GetNextFreeThermostatId()
        {
            var databaseThermostatService = new DatabaseThermostatService();
            var allThermostatIds = databaseThermostatService.GetAllThermostatIds();
           
                int? firstAvailable = Enumerable.Range(1, int.MaxValue)
                                .Except(allThermostatIds)
                                .FirstOrDefault();

                return firstAvailable;
        }

        public static int? GetNextFreeRoomId()
        {
            var databaseRoomService = new DatabaseRoomService();
            var allRoomIds = databaseRoomService.GetAllRoomIds();

           
                int? firstAvailable = Enumerable.Range(1, int.MaxValue)
                                .Except(allRoomIds)
                                .FirstOrDefault();

                return firstAvailable;
        }

        public static string? SafeGetString(MySqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return null;
        }

        public static int? SafeGetInt(MySqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetInt32(colIndex);
            return null;
        }

        public static double? SafeGetDouble(MySqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetDouble(colIndex);
            return null;
        }
    }
}
