using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GeocachingApi.Infrastructure.Interfaces;
using GeocachingApi.Infrastructure.Models;

namespace GeocachingApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeocacheItemController : ControllerBase
    {
        private readonly ILogger<GeocacheItemController> _logger;
        private readonly IGeocacheItemService geocacheItemService;

        public GeocacheItemController(ILogger<GeocacheItemController> logger, IGeocacheItemService geocacheItemService)
        {
            _logger = logger;
            this.geocacheItemService = geocacheItemService;
        }

        [HttpGet("GetActiveGeocacheItemsByGeocacheId/{id}"), Produces(typeof(Geocache))]
        public async Task<ActionResult<Geocache>> GetActiveGeocacheItemsByGeocacheId(int id)
        {
            if (id == 0)
            {
                this.ModelState.AddModelError(nameof(id), "Invalid Id provided.");
                return this.BadRequest(this.ModelState);
            }

            try
            {
                var geocache = await this.geocacheItemService.GetActiveGeocacheItemsByGeocacheId(id);
                if (!geocache.Any())
                {
                    return NotFound();
                }

                return this.Ok(geocache);

            } catch (Exception e)
            {
                _logger.LogError(e.InnerException.ToString());
                return this.BadRequest(e.Message);
            }
        }
    }
}
