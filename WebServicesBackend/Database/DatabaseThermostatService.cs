namespace WebServicesBackend.Database
{
    public class DatabaseThermostatService
    {
        public Tuple<bool,int?> AddThermostat()
        {
            //TODO implement creation stuff and after thermostat was created succesfully return true and the ID of the created item 
            return new Tuple<bool, int?>(true, 1);
        }

        public bool DeleteThermostat(int thermostatId)
        {
            //TODO implement deleting stuff and after deleting thermostat return true 
            return true;
        }
    }
}
