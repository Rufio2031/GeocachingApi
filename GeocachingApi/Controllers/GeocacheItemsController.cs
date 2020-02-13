using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
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

        /// <summary>
        /// Get geocache item by Id.
        /// </summary>
        /// <param name="id">The Id of the geocache item</param>
        /// <returns>ObjectResult with GeocacheItemModel of the geocache item.</returns>
        [HttpGet("{id}"), Produces(typeof(GeocacheItemModel))]
        public async Task<ActionResult> GetGeocacheItem(int id)
        {
            if (id <= 0)
            {
                this.ModelState.AddModelError(nameof(id), "Invalid Id provided.");
                return this.BadRequest(this.ModelState);
            }

            try
            {
                var geocacheItem = await this.geocacheItemsService.GetGeocacheItem(id);
                if (geocacheItem.Id <= 0)
                {
                    var noResultsFoundMessage = $"No GeocacheItem found for Id {id}";
                    return NotFound(noResultsFoundMessage);
                }

                return this.Ok(geocacheItem);

            }
            catch (Exception e)
            {
                this.logger.LogError(e.InnerException?.ToString());
                return this.BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get list of geocache items found associated to the given geocacheId.
        /// </summary>
        /// <param name="geocacheId">The geocache id.</param>
        /// <param name="activeOnly"><c>true</c> to only include active geocache items; otherwise, <c>false</c> include all results.</param>
        /// <returns>ObjectResult with list of GeocacheItemModel of the given geocacheId.</returns>
        [HttpGet("~/geocaches/{geocacheId}/geocache-items"), Produces(typeof(List<GeocacheItemModel>))]
        public async Task<ActionResult> GetGeocacheItemsByGeocacheId(int geocacheId, bool activeOnly = true)
        {
            if (geocacheId <= 0)
            {
                this.ModelState.AddModelError(nameof(geocacheId), "Invalid Id provided.");
                return this.BadRequest(this.ModelState);
            }

            try
            {
                var geocacheItems = await this.geocacheItemsService.GetGeocacheItemsByGeocacheId(geocacheId, activeOnly);
                if (!geocacheItems.Any())
                {
                    var noResultsFoundMessage = $"No GeocacheItems found for GeocacheId {geocacheId}";
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

        /// <summary>
        /// Creates a new geocache item with the given geocache item data.
        /// </summary>
        /// <param name="geocacheItem">The geocache item to create.</param>
        /// <returns>ObjectResult with the created geocache item.</returns>
        [HttpPost, Produces(typeof(GeocacheItemModel))]
        public async Task<ActionResult> CreateGeocacheItem([FromBody]GeocacheItemModel geocacheItem)
        {
            try
            {
                var validationMessage = await this.geocacheItemsService.ValidateGeocacheItem(geocacheItem);

                if (validationMessage.Any())
                {
                    this.ModelState.AddModelError(nameof(geocacheItem), JsonSerializer.Serialize(validationMessage));
                    return this.BadRequest(this.ModelState);
                }

                geocacheItem = (GeocacheItemModel)await this.geocacheItemsService.CreateGeocacheItem(geocacheItem);

                return this.Ok(geocacheItem);
            }
            catch (Exception e)
            {
                this.logger.LogError(e.InnerException?.ToString());
                return this.BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Updates the GeocacheId of the given Geocache item id.
        /// </summary>
        /// <param name="patchModel">The patch model to update the GeocacheId.</param>
        /// <returns>ObjectResult with GeocacheItemModel of the updated geocache item.</returns>
        [HttpPatch, Produces(typeof(GeocacheItemModel))]
        public async Task<ActionResult> PatchGeocacheId([FromBody]GeocacheItemPatchGeocacheIdModel patchModel)
        {
            if (patchModel.Id <= 0)
            {
                this.ModelState.AddModelError(nameof(patchModel.Id), "Invalid Id provided.");
                return this.BadRequest(this.ModelState);
            }
            if (patchModel.GeocacheId <= 0)
            {
                this.ModelState.AddModelError(nameof(patchModel.GeocacheId), "Invalid GeocacheId provided.");
                return this.BadRequest(this.ModelState);
            }

            try
            {
                var validationForUpdateMessages = await this.geocacheItemsService.ValidateForPatchGeocacheId(patchModel);
                if (validationForUpdateMessages.Any())
                {
                    this.ModelState.AddModelError(nameof(patchModel), JsonSerializer.Serialize(validationForUpdateMessages));
                    return this.BadRequest(this.ModelState);
                }

                var geocacheItem = (GeocacheItemModel)await this.geocacheItemsService.PatchGeocacheItemGeocacheId(patchModel);

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