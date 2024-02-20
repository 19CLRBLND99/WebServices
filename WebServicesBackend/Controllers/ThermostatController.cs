using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebServicesBackend.Services;

namespace WebServicesBackend.Controllers
{
    /// <summary>
    /// Controller for all the thermostat related endpoints 
    /// </summary>
    public class ThermostatController : ControllerBase
    {
        [Route("/AddThermostat")]
        [HttpPost]
        public IActionResult AddThermostat()
        {
            var thermostatService = new ThermostatService();
            var result = thermostatService.AddThermostat();
            return (result.Item1) ? Ok(result.Item2) : BadRequest();
        }

        [Route("/DeleteThermostat")]
        [HttpDelete]
        public IActionResult DeleteThermostat(int thermostatId)
        {
            var thermostatService = new ThermostatService();
            var result = thermostatService.DeleteThermostat(thermostatId);
            return (result) ? Ok() : BadRequest();
        }
    }
}
