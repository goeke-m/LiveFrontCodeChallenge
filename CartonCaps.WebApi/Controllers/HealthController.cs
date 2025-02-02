using Microsoft.AspNetCore.Mvc;

namespace CartonCaps.WebApi.Controllers
{
    /// <summary>
    /// Controller to handle health check requests.
    /// </summary>
    [ApiController]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// Endpoint to check the health of the application.
        /// </summary>
        /// <returns>Returns an Ok result if the application is healthy.</returns>
        [HttpGet]
        [Route("api/v{apiVersion:apiVersion}/health")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult HealthCheck()
        {
            return Ok();
        }
    }
}
