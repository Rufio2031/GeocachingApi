using System;
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
    public class GeocachesController : ControllerBase
    {
        private readonly ILogger<GeocachesController> logger;
        private readonly IGeocachesService geocacheService;

        public GeocachesController(ILogger<GeocachesController> logger, IGeocachesService geocacheService)
        {
            this.logger = logger;
            this.geocacheService = geocacheService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeocacheModel>>> Get()
        {
            try
            {
                var geocaches = await this.geocacheService.GetGeocaches();

                return this.Ok(geocaches);

            } catch (Exception e)
            {
                this.logger.LogError(e.InnerException?.ToString());
                return this.BadRequest(e.Message);
            }
        }

        [HttpGet("{id}"), Produces(typeof(GeocacheModel))]
        public async Task<ActionResult<GeocacheModel>> Get(int id)
        {
            if (id == 0)
            {
                this.ModelState.AddModelError(nameof(id), "Invalid Id provided.");
                return this.BadRequest(this.ModelState);
            }

            try
            {
                var geocache = await this.geocacheService.GetGeocache(id);
                if (geocache?.Id == 0)
                {
                    return NotFound();
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