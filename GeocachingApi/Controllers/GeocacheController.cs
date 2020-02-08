using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GeocachingApi.DataAccess;
using GeocachingApi.Domain.Interfaces;
using GeocachingApi.Domain.Models;
using GeocachingApi.Domain.Services;

namespace GeocachingApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeocacheController : ControllerBase
    {
        private readonly ILogger<GeocacheController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IGeocacheService geocacheService;

        public GeocacheController(ApplicationDbContext context, ILogger<GeocacheController> logger, IGeocacheService geocacheService)
        {
            _logger = logger;
            _context = context;
            this.geocacheService = geocacheService;
        }

        // GET: api/Geocache
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Geocache>>> Get()
        {
            //return await _context.Geocache2.Include(x => x.Id).ToListAsync();
            var geocaches = this.geocacheService.GetActiveGeocaches();
            if (!geocaches.Any())
            {
                return this.NotFound(new List<Geocache>());
            }

            return this.Ok(geocaches);
        }

        // GET: api/Geocache/2
        [HttpGet("{id}"), Produces(typeof(Geocache))]
        public async Task<ActionResult<Geocache>> Get(int id)
        {
            if (id == 0)
            {
                this.ModelState.AddModelError(nameof(id), "Invalid Id");
                return this.BadRequest(this.ModelState);
            }

            //var user = await _context.Geocache2.FindAsync(id);
            var geocache = this.geocacheService.GetGeocacheById(id);
            if (geocache == null)
            {
                return NotFound();
            }

            return this.Ok(geocache);
        }
    }
}
