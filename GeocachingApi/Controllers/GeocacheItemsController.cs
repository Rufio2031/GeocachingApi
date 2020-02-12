using System;
using System.Text.Json;
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
        private readonly ILogger<GeocacheItemsController> logger;
        private readonly IGeocacheItemsService geocacheItemsService;

        public GeocacheItemsController(ILogger<GeocacheItemsController> logger, IGeocacheItemsService geocacheItemsService)
        {
            this.logger = logger;
            this.geocacheItemsService = geocacheItemsService;
        }

        [HttpGet("~/geocaches/{id}/geocache-items"), Produces(typeof(GeocacheItemModel))]
        public async Task<ActionResult<GeocacheItemModel>> GetGeocacheItemsByGeocacheId(int id, bool activeOnly = true)
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
                this.logger.LogError(e.InnerException?.ToString());
                return this.BadRequest(e.Message);
            }
        }

        [HttpPost, Produces(typeof(GeocacheItemModel))]
        public async Task<ActionResult<GeocacheItemModel>> AddGeocacheItem([FromBody]GeocacheItemModel geocacheItem)
        {
            if (geocacheItem == null)
            {
                this.ModelState.AddModelError(nameof(geocacheItem), "Invalid Geocache Item.");
                return this.BadRequest(this.ModelState);
            }

            var validationMessage = await this.geocacheItemsService.ValidateGeocacheItem(geocacheItem);

            if (validationMessage.Any())
            {
                this.ModelState.AddModelError(nameof(geocacheItem), JsonSerializer.Serialize(validationMessage));
                return this.BadRequest(this.ModelState);
            }

            try
            {
                geocacheItem = (GeocacheItemModel)await this.geocacheItemsService.CreateGeocacheItem(geocacheItem);

                return this.Ok(geocacheItem);
            }
            catch (Exception e)
            {
                this.logger.LogError(e.InnerException?.ToString());
                return this.BadRequest(e.Message);
            }
        }
    }
}