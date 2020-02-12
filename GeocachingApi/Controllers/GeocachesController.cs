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

        // GET: /Geocache
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeocacheModel>>> Get()
        {
            try
            {
                var geocaches = await this.geocacheService.GetActiveGeocaches();

                return this.Ok(geocaches);

            } catch (Exception e)
            {
                this.logger.LogError(e.InnerException.ToString());
                return this.BadRequest(e.Message);
            }
        }

        // GET: /Geocache/2
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
                var geocache = await this.geocacheService.GetGeocacheById(id);
                if (geocache?.Id == 0)
                {
                    return NotFound();
                }

                return this.Ok(geocache);

            } catch (Exception e)
            {
                this.logger.LogError(e.InnerException.ToString());
                return this.BadRequest(e.Message);
            }
        }
    }
}
