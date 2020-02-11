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
        private readonly IGeocacheItemsService geocacheItemsService;

        public GeocacheItemsController(ILogger<GeocacheItemsController> logger, IGeocacheItemsService geocacheItemsService)
        {
            _logger = logger;
            this.geocacheItemsService = geocacheItemsService;
        }

        [HttpGet("~/geocaches/{id}/geocache-items"), Produces(typeof(GeocacheItem))]
        public async Task<ActionResult<GeocacheItem>> GetGeocacheItemsByGeocacheId(int id, bool activeOnly = true)
        {
            if (id == 0)
            {
                this.ModelState.AddModelError(nameof(id), "Invalid Id provided.");
                return this.BadRequest(this.ModelState);
            }

            try
            {
                var geocacheItems = await this.geocacheItemsService.GetGeocacheItemsByGeocacheId(id, activeOnly);
                if (!geocacheItems.Any())
                {
                    var noResultsFoundMessage = $"No GeocacheItems found for GeocacheId {id}";
                    return NotFound(noResultsFoundMessage);
                }

                return this.Ok(geocacheItems);

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
                var geocache = await this.geocacheItemsService.GetGeocacheItemsByGeocacheId(geocacheItem.Id, false);
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