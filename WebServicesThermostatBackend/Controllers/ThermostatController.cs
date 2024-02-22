using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThermostatBackend.Service;

namespace ThermostatBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ThermostatController : ControllerBase
    {
        [Route("/updateTemperature")]
        [HttpPost]

        public IActionResult updateTemperature(float temperature)
        {
            var result = ThermostatService.updateTemperature(temperature);
            return (result.Item1) ? Ok(result.Item2) : BadRequest();
        }
        [Route("/setThermostatId")]
        [HttpPost]
        public IActionResult setThermostatId(int id)
        {
            var result = ThermostatService.setID(id);
            return (result.Item1) ? Ok(result.Item2) : BadRequest();
        }
    }
}
