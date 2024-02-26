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
    }
}
