namespace ThermostatBackend.Model
{
    public static class ThermostatModel
    {

        private static float temperature = 0;
        private static int ownId = 0;
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
    }
}
