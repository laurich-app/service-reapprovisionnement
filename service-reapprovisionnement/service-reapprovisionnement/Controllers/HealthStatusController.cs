using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace service_reapprovisionnement.Controllers;
[Route("health")]
[ApiController]
[AllowAnonymous]
public class HealthStatusController: ControllerBase
{

		[HttpGet()]
    public IActionResult Get()
    {
        return Ok("Healthy");
    }
}