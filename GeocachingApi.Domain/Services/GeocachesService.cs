using System.Collections.Generic;
using System.Threading.Tasks;
using GeocachingApi.Infrastructure.Interfaces;
using GeocachingApi.Infrastructure.Models;
using GeocachingApi.Infrastructure.Extensions;

namespace GeocachingApi.Domain.Services
{
    public class GeocachesService : IGeocachesService
    {
        private readonly IDataService dataService;

        public GeocachesService(IDataService dataService)
        {
            this.dataService = dataService;
        }

        public async Task<IList<GeocacheModel>> GetGeocaches()
        {
            var geocaches = await this.dataService.GetGeocaches();

            return geocaches.ToSafeList();
        }

        public async Task<GeocacheModel> GetGeocache(int id)
        {
            var geocache = await this.dataService.GetGeocache(id);

            return geocache ?? new GeocacheModel();
        }
    }
}