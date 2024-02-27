﻿using Microsoft.AspNetCore.Mvc;
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
        [Route("/GetAllThermostatIds")]
        [HttpGet]
        public IActionResult GetAllThermostatIds()
        {
            var thermostatService = new ThermostatService();
            var result = thermostatService.GetAllThermostatIds();
            if(result.Count() > 0)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [Route("/GetAllFreeThermostatIds")]
        [HttpGet]
        public IActionResult GetAllFreeThermostatIds()
        {
            var thermostatService = new ThermostatService();
            var result = thermostatService.GetAllFreeThermostatIds();
            if (result.Count() > 0)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [Route("/CheckThermostatId")]
        [HttpGet]
        public IActionResult CheckThermostatId(int thermostatId)
        {
            var thermostatService = new ThermostatService();
            var result = thermostatService.CheckThermostatId(thermostatId);
            return Ok(result);
        }

    }
}
