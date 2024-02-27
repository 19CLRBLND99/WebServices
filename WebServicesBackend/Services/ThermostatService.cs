using WebServicesBackend.Database;
using WebServicesBackend.HelperFunctions;

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
            var newId = HelperFunctionsClass.GetNextFreeThermostatId();

            var thermostatDbService = new DatabaseThermostatService();
            var result = thermostatDbService.AddThermostat(newId);

            return (result.Item1) ? result : new Tuple<bool, int?>(false, null);
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

        public bool SetThermostatTemperature(int? possibleThermostatId, double newTemperature)
        {
            if (possibleThermostatId == null)
            {
                return false;
            }
            var thermostatId = Convert.ToInt32(possibleThermostatId);
            Console.WriteLine("ThermostatId: " + thermostatId);
            var thermostatPort = 50000 + thermostatId;
            Console.WriteLine("ThermostatPort:" + thermostatPort);

            //change hardcoded url url that fits containers
            var thermostatUrl = $"https://localhost:{thermostatPort}/updateTemperature?temperature={newTemperature}";

            var thermostatDbService = new DatabaseThermostatService();
            var DbResult = thermostatDbService.SetThermostatTemperatureInDB(thermostatId, newTemperature);

            var ThermostatResult = UpdateThermostatTemperatureDirectlyAtThermostat(thermostatUrl);

            if (DbResult && ThermostatResult.Result)
            {
                return true;
            }
            return false;
        }

        public List<int> GetAllThermostatIds()
        {
            var thermostatDbService = new DatabaseThermostatService();
            var result = thermostatDbService.GetAllThermostatIds();
            return result;
        }



        static async Task<bool> UpdateThermostatTemperatureDirectlyAtThermostat(string apiUrl)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            using (HttpClient httpClient = new HttpClient(clientHandler))
            {
                try
                {
                    HttpResponseMessage response = await httpClient.PostAsync(apiUrl, null);
                    Console.WriteLine("response: " + response);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Successfully updated Thermostat temperature");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Error while updating Thermostat temperature: {response.StatusCode} - {response.ReasonPhrase}");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while updating Thermostat temperature: {ex.Message}");
                    return false;
                }
            }
        }



        public List<int> GetAllFreeThermostatIds()
        {
            var thermostatDbService = new DatabaseThermostatService();
            var roomsDbService = new DatabaseRoomService();

            var allThermostatIds = thermostatDbService.GetAllThermostatIds();
            var res = roomsDbService.GetAllAssignedThermostatIds();
            var allFreeThermostatIds = new List<int>();

            foreach (var id in allThermostatIds)
            {
                if (!res.Contains(id))
                {
                    allFreeThermostatIds.Add(id);
                }
            }
            return allFreeThermostatIds;
        }


        public Tuple<bool, List<int>?> CheckThermostatId(int thermostatId)
        {
            var thermostatDbService = new DatabaseThermostatService();
            var roomsDbService = new DatabaseRoomService();

            var assignedIds = roomsDbService.GetAllAssignedThermostatIds();

            foreach (var id in assignedIds) 
            { 
                if (id == thermostatId)
                {
                    return new Tuple<bool, List<int>?>(false, GetAllFreeThermostatIds());
                }
            }
            return new Tuple<bool,List<int>?>(true,null);
        }

    }

}

