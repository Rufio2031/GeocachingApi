using System.Collections.Generic;
using System.Threading.Tasks;
using GeocachingApi.Domain.Interfaces;
using GeocachingApi.Domain.Models;
using GeocachingApi.DataAccess;

namespace GeocachingApi.Domain.Services
{
    public class DataService : IDataService
    {
        private readonly ApplicationDbContext _context;

        public DataService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Geocache>> GetActiveGeocaches(int id)
        {
            var geocaches = new List<Geocache>();

            geocaches = await _context.Geocache2.Include(x => x.Id).ToListAsync();

            return new List<Geocache>();
        }
    }
}
