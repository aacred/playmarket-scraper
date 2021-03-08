using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Domain.Common.Exceptions;
using InputApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InputApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppController : ControllerBase
    {
        private readonly ILogger<AppController> _logger;
        private readonly IApplicationService _applicationService;

        public AppController(ILogger<AppController> logger, IApplicationService applicationService)
        {
            _logger = logger;
            _applicationService = applicationService ?? throw new ArgumentNullException(nameof(IApplicationService));
        }

        [HttpPost]
        public async Task<IActionResult> Add([Required] [Url]
            string url = "https://play.google.com/store/apps/details?id=com.instagram.android&hl=ru&gl=US")
        {
            if (ModelState.IsValid)
            {
                long id = await _applicationService.AddOrGetId(url);
                return Ok(id);
            }

            return BadRequest();
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([Required] int id = 1)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            try
            {
                var details = await _applicationService.GetDetails(id);
                return Ok(details);
            }
            catch (Exception e) when(e is ApplicationNotFoundException || 
                                     e is ApplicationDetailsNotFoundException)
            {
                return NotFound(e.Message);
            }
        }
    }
}