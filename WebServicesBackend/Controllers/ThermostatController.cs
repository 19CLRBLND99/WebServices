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
        /// <summary>
        /// Controller Endpoint for adding a new thermostat
        /// </summary>
        /// <returns>
        /// IActionResult Ok() - thermostat was sucessfully added
        /// IActionResult BadRequest() - problem while adding the thermostat
        /// </returns>
        [Route("/AddThermostat")]
        [HttpPost]
        public IActionResult AddThermostat()
        {
            var thermostatService = new ThermostatService();
            var result = thermostatService.AddThermostat();
            return (result.Item1) ? Ok(result.Item2) : BadRequest();
        }

        /// <summary>
        /// Controller Endpoint for deleting a thermostat
        /// </summary>
        /// <param name="thermostatId">the id of the thermostat to delete</param>
        /// <returns>
        /// IActionResult Ok() - thermostat was sucessfully deleted
        /// IActionResult BadRequest() - problem while deleting the thermostat
        /// </returns>
        [Route("/DeleteThermostat")]
        [HttpDelete]
        public IActionResult DeleteThermostat(int thermostatId)
        {
            var thermostatService = new ThermostatService();
            var result = thermostatService.DeleteThermostat(thermostatId);
            return (result) ? Ok() : BadRequest();
        }

        /// <summary>
        /// Controller Endpoint for getting all thermostatIds that are in the database
        /// </summary>
        /// <returns>
        /// IActionResult Ok() - successfully fetched all Ids
        /// IActionResult BadRequest() - problem while fetcheding all Ids
        /// </returns>
        [Route("/GetAllThermostatIds")]
        [HttpGet]
        public IActionResult GetAllThermostatIds()
        {
            var thermostatService = new ThermostatService();
            var result = thermostatService.GetAllThermostatIds();
            return (result.Count() > 0) ? Ok(result) : BadRequest();
        }

        /// <summary>
        /// Controller Endpoint for getting all free thermostatIds that are in the database
        /// </summary>
        /// <returns>
        /// IActionResult Ok() - successfully fetched all free Ids
        /// IActionResult BadRequest() - problem while fetcheding all free Ids
        /// </returns>
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

        /// <summary>
        /// Controller Endpoint for checking whether a thermostatId can still be assigned or not
        /// </summary>
        /// <param name="thermostatId">the thermostatId to check</param>
        /// <returns>
        /// IActionResult Ok(Tuple<bool,List<int>?>(true,null)) - Id can still be assigned
        /// IActionResult Ok(Tuple<bool,List<int>?>(false,[1,2,3,...])) - Id can not be assigned
        /// </returns>
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
