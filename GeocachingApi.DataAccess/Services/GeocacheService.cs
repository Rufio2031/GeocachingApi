using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeocachingApi.Domain.Interfaces;
using GeocachingApi.Domain.Models;

namespace GeocachingApi.Domain.Services
{
    public class GeocacheService : IGeocacheService
    {
        private readonly IDataService dataService;

        public GeocacheService(IDataService dataService)
        {
            this.dataService = dataService;
        }

        public async Task<IEnumerable<Geocache>> GetActiveGeocaches(int id)
        {
            var geocaches = new List<Geocache>();

            geocaches = await this.dataService.GetActiveGeocaches(id).ToList();

            return new List<Geocache>();
        }

        public Geocache GetGeocacheById(int id)
        {

            return new Geocache();
        }
    }
}
