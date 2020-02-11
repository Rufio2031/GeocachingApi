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
        private readonly ILogger<GeocachesController> _logger;
        private readonly IGeocacheService geocacheService;

        public GeocachesController(ILogger<GeocachesController> logger, IGeocacheService geocacheService)
        {
            _logger = logger;
            this.geocacheService = geocacheService;
        }

        // GET: /Geocache
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Geocache>>> Get()
        {
            try
            {
                var geocaches = await this.geocacheService.GetActiveGeocaches();

                return this.Ok(geocaches);

            } catch (Exception e)
            {
                _logger.LogError(e.InnerException.ToString());
                return this.BadRequest(e.Message);
            }
        }

        // GET: /Geocache/2
        [HttpGet("{id}"), Produces(typeof(Geocache))]
        public async Task<ActionResult<Geocache>> Get(int id)
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
                _logger.LogError(e.InnerException.ToString());
                return this.BadRequest(e.Message);
            }
        }
    }
}
