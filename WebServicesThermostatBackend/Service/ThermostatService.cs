using System.Security.Cryptography;
using ThermostatBackend.Model;

namespace ThermostatBackend.Service
{
    public class ThermostatService
    {

        public static Tuple<bool,float> updateTemperature(float temperature)
        {
            var result = ThermostatModel.setTemperature(temperature);
            return (result.Item1) ? result: new Tuple<bool, float>(false, float.NaN);
        }

        public static Tuple<bool, int?> setID(int newId)
        {
            var result = ThermostatModel.setID(newId);
            return (result.Item1) ? result : new Tuple<bool, int?>(false, null);
        }
    }
}
