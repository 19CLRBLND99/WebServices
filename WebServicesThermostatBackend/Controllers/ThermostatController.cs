using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThermostatBackend.Service;

namespace ThermostatBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ThermostatController : ControllerBase
    {
        //if the action was successful, depends on the first part of the tuple and if its false, a bad Request is returned
        [Route("/updateTemperature")]
        [HttpPost]
        public IActionResult updateTemperature(double temperature)
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

        [Route("/getTemperature")]
        [HttpGet]

        public IActionResult getTemperature()
        {
            var result = ThermostatService.getTemperature();
            return (result.Item1) ? Ok(result.Item2) : BadRequest();
        }
        [Route("/getThermostatId")]
        [HttpGet]
        public IActionResult getThermostatId()
        {
            var result = ThermostatService.getID();
            return (result.Item1) ? Ok(result.Item2) : BadRequest();
        }
    }
}
