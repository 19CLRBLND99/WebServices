namespace WebServicesBackend.Models
{
    public class RoomWithThermostatModel
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int ThermostatId { get; set; }
        public ThermostatModel? Thermostat { get; set; }
    }
}
