using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using System.Security.Claims;
using Securities;

namespace service_reapprovisionnement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsHelper _claimsHelper;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpContextAccessor httpContextAccessor, ClaimsHelper claimsHelper)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _claimsHelper = claimsHelper;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("test")]
        [AllowAnonymous]
        public IEnumerable<WeatherForecast> GetTest()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("test2")]
        public IEnumerable<WeatherForecast> GetTest2()
        {
            var principal = _httpContextAccessor.HttpContext.User;

            // Exemple, pour récupéré l'ID de l'utilisateur
            Console.WriteLine(this._claimsHelper.GetName((ClaimsIdentity)principal.Identity));
            // Pour récupérer tous ses rôles
            Console.WriteLine(this._claimsHelper.GetRoles((ClaimsIdentity)principal.Identity));
            // Pour savoir s'il est admin
            Console.WriteLine(this._claimsHelper.IsGestionaire((ClaimsIdentity)principal.Identity));

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
