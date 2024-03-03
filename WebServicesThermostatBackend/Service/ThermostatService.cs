using System.Security.Cryptography;
using ThermostatBackend.Model;

namespace ThermostatBackend.Service
{
    public class ThermostatService
    {
        //all of them return tuples because the first part shows if the action was successful
        //and the other returns the changed value or the requested value
        public static Tuple<bool, double> updateTemperature(double temperature)
        {
            var result = ThermostatModel.setTemperature(temperature);
            return (result.Item1) ? result: new Tuple<bool, double>(false, double.NaN);
        }

        public static Tuple<bool, int?> setID(int newId)
        {
            var result = ThermostatModel.setID(newId);
            return (result.Item1) ? result : new Tuple<bool, int?>(false, null);
        }

        public static Tuple<bool, int?> getID()
        {
            var result = ThermostatModel.getID();
            return (result.Item1) ? result : new Tuple<bool, int?>(false, null);
        }

        public static Tuple<bool, double> getTemperature()
        {
            var result = ThermostatModel.getTemperature();
            return (result.Item1) ? result : new Tuple<bool, double>(false, double.NaN);
        }
    }
}
