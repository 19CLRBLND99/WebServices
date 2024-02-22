namespace ThermostatBackend.Model
{
    public static class ThermostatModel
    {

        private static float temperature;
        private static int ownId;
        public static Tuple<bool, float> setTemperature(float temperatureUpdate)
        {
            float tempTemperature = temperature;
            temperature = temperatureUpdate;
            Console.WriteLine("The temperature of thermostat " + ownId + " got changed from " + tempTemperature + " to " + temperature + ".");
            return new Tuple<bool,float>(true,temperature);
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

        public static Tuple<bool, float> getTemperature()
        {
            return new Tuple<bool, float>(true, temperature);
        }
    }
}
