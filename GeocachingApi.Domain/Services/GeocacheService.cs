using System.Collections.Generic;
using System.Threading.Tasks;
using GeocachingApi.Infrastructure.Interfaces;
using GeocachingApi.Infrastructure.Models;
using GeocachingApi.Infrastructure.Extensions;

namespace GeocachingApi.Domain.Services
{
    public class GeocacheService : IGeocacheService
    {
        private readonly IDataService dataService;

        public GeocacheService(IDataService dataService)
        {
            this.dataService = dataService;
        }

        public async Task<IList<Geocache>> GetActiveGeocaches()
        {
            var geocaches = await this.dataService.GetActiveGeocaches();

            return geocaches.ToSafeList();
        }

        public async Task<Geocache> GetGeocacheById(int id)
        {
            var geocache = await this.dataService.GetGeocacheById(id);

            return geocache ?? new Geocache();
        }
    }
}