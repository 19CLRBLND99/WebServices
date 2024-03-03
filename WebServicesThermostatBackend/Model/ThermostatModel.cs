namespace ThermostatBackend.Model
{
    public static class ThermostatModel
    {
        //the class is static, because there is only one thermostat per container so there will only be one instance of the thermostat
        private static double temperature;
        private static int ownId;
        public static Tuple<bool, double> setTemperature(double temperatureUpdate)
        {
            double tempTemperature = temperature;
            temperature = temperatureUpdate;
            //it was not clear which console should print the statement, so I used the console of the IDE
            Console.WriteLine("The temperature of thermostat " + ownId + " got changed from " + tempTemperature + " to " + temperature + ".");
            return new Tuple<bool, double>(true,temperature);
        }

        public static Tuple<bool, int?> setID(int newId)
        {
            ownId = newId;
            return new Tuple<bool, int?>(true,ownId);
        }

        public static Tuple<bool, int?> getID()
        {
            return new Tuple<bool, int?>(true, ownId);
        }

        public static Tuple<bool, double> getTemperature()
        {
            return new Tuple<bool, double>(true, temperature);
        }
    }
}
