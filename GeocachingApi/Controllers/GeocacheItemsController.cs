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
    public class GeocacheItemsController : ControllerBase
    {
        private readonly ILogger<GeocacheItemsController> _logger;
        private readonly IGeocacheItemsService geocacheItemService;

        public GeocacheItemsController(ILogger<GeocacheItemsController> logger, IGeocacheItemsService geocacheItemService)
        {
            _logger = logger;
            this.geocacheItemService = geocacheItemService;
        }

        [HttpGet("~/geocaches/{id}/geocache-items"), Produces(typeof(Geocache))]
        public async Task<ActionResult<Geocache>> GetGeocacheItemsByGeocacheId(int id, bool activeOnly = true)
        {
            if (id == 0)
            {
                this.ModelState.AddModelError(nameof(id), "Invalid Id provided.");
                return this.BadRequest(this.ModelState);
            }

            try
            {
                var geocache = await this.geocacheItemService.GetGeocacheItemsByGeocacheId(id, activeOnly);
                if (!geocache.Any())
                {
                    var noResultsFoundMessage = $"No Results found for GeocacheId {id}";
                    return NotFound(noResultsFoundMessage);
                }

                return this.Ok(geocache);

            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException?.ToString());
                return this.BadRequest(e.Message);
            }
        }

        [HttpPost, Produces(typeof(GeocacheItem))]
        public async Task<ActionResult<GeocacheItem>> AddGeocacheItem([FromBody]GeocacheItem geocacheItem)
        {
            try
            {
                var geocache = await this.geocacheItemService.GetActiveGeocacheItemsByGeocacheId(geocacheItem.Id);
                return this.Ok();
                if (!geocache.Any())
                {
                    return NotFound();
                }

                return this.Ok(geocache);

            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.ToString());
                return this.BadRequest(e.Message);
            }
        }
    }
}