using System;
using System.Collections.Generic;
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
    public class GeocachesController : ControllerBase
    {
        private readonly ILogger<GeocachesController> logger;
        private readonly IGeocachesService geocacheService;

        public GeocachesController(ILogger<GeocachesController> logger, IGeocachesService geocacheService)
        {
            this.logger = logger;
            this.geocacheService = geocacheService;
        }

        /// <summary>
        /// Get all geocaches.
        /// </summary>
        /// <returns>ObjectResult with List of GeocacheModel of the geocaches.</returns>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var geocaches = await this.geocacheService.GetGeocaches();
                if (!geocaches.Any())
                {
                    var noResultsFoundMessage = $"No Geocaches found.";
                    return NotFound(noResultsFoundMessage);
                }

                return this.Ok(geocaches);

            } catch (Exception e)
            {
                this.logger.LogError(e.InnerException?.ToString());
                return this.BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get geocache by given id.
        /// </summary>
        /// <param name="id">The geocache id to lookup.</param>
        /// <returns>ObjectResult with GeocacheModel of the geocache.</returns>
        [HttpGet("{id}"), Produces(typeof(GeocacheModel))]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                this.ModelState.AddModelError(nameof(id), "Invalid Id provided.");
                return this.BadRequest(this.ModelState);
            }

            try
            {
                var geocache = await this.geocacheService.GetGeocache(id);
                if (geocache?.Id == 0)
                {
                    var noResultFoundMessage = $"No Geocache found for id {id}.";
                    return NotFound(noResultFoundMessage);
                }

                return this.Ok(geocache);

            } catch (Exception e)
            {
                this.logger.LogError(e.InnerException?.ToString());
                return this.BadRequest(e.Message);
            }
        }
    }
}