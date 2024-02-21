using WebServicesBackend.Database;

namespace WebServicesBackend.Services
{
    public class ThermostatService
    {
        /// <summary>
        /// Method which will create a new thermostat
        /// </summary>
        /// <returns>a Tuple which contains a boolean and a nullable int. The boolean and int indicate whether the creation was successfull or not</returns>
        public Tuple<bool, int?> AddThermostat()
        {
            var thermostatDbService = new DatabaseThermostatService();
            var result = thermostatDbService.AddThermostat();

            return (result.Item1) ? result : new Tuple<bool, int?>(false,null);
        }

        /// <summary>
        /// Method which will delete a already created Thermostat 
        /// </summary>
        /// <returns>a boolean which indicates whether the deleting process was successfull or not</returns>
        public bool DeleteThermostat(int thermostatId)
        {

            //Check for valid ID!
            var thermostatDbService = new DatabaseThermostatService();
            var result = thermostatDbService.DeleteThermostat(thermostatId);

            return (result) ? true : false; 
        }

        //TODO REWORK 
        public bool SetThermostatTemperature(int? thermostatId, double newTemperature)
        {
            if (thermostatId == null)
            {
                return false;
            }
            var thermostatDbService = new DatabaseThermostatService();
            var result = thermostatDbService.SetThermostatTemperature(thermostatId, newTemperature);
            Console.WriteLine(newTemperature); //TODO  api (http put/post/..)  call an niklas API welche dann die temperature und das thermometer in die console logt ???
            return true;
        }
    }
}
