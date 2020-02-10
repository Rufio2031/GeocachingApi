using System.Collections.Generic;
using System.Threading.Tasks;
using GeocachingApi.Infrastructure.Interfaces;
using GeocachingApi.Infrastructure.Models;
using GeocachingApi.Infrastructure.Extensions;

namespace GeocachingApi.Domain.Services
{
    public class GeocacheItemService : IGeocacheItemService
    {
        private readonly IDataService dataService;

        public GeocacheItemService(IDataService dataService)
        {
            this.dataService = dataService;
        }

        public async Task<IList<GeocacheItem>> GetActiveGeocacheItemsByGeocacheId(int id)
        {
            var geocaches = await this.dataService.GetActiveGeocacheItemsByGeocacheId(id);

            return geocaches.ToSafeList();
        }
    }
}