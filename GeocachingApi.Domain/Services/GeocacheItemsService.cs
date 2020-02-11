using System.Collections.Generic;
using System.Threading.Tasks;
using GeocachingApi.Infrastructure.Interfaces;
using GeocachingApi.Infrastructure.Models;
using GeocachingApi.Infrastructure.Extensions;

namespace GeocachingApi.Domain.Services
{
    public class GeocacheItemsService : IGeocacheItemsService
    {
        private readonly IDataService dataService;

        public GeocacheItemsService(IDataService dataService)
        {
            this.dataService = dataService;
        }

        public async Task<IList<GeocacheItem>> GetGeocacheItemsByGeocacheId(int id, bool activeOnly)
        {
            var geocacheItems = await this.dataService.GetGeocacheItemsByGeocacheId(id, activeOnly);

            return geocacheItems.ToSafeList();
        }
    }
}