using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GeocachingApi.DataAccess;
using GeocachingApi.Domain.Models;

namespace GeocachingApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeocacheController : ControllerBase
    {
        private readonly ILogger<GeocacheController> _logger;
        private readonly ApplicationDbContext _context;

        public GeocacheController(ApplicationDbContext context, ILogger<GeocacheController> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/Geocache
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Geocache>>> GetGeocaches()
        {
            return await _context.Geocache2.Include(x => x.Id).ToListAsync();
        }

        // GET: api/Geocache/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Geocache>> GetGeocache(int id)
        {
            var user = await _context.Geocache2.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
    }
}
