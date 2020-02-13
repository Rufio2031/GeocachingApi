using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeocachingApi.Infrastructure.Interfaces;
using GeocachingApi.Infrastructure.Models;
using GeocachingApi.Domain.DataAccess.Geocaching;
using GeocachingApi.Domain.Factories;
using GeocachingApi.Domain.Queries;
using GeocachingApi.Infrastructure.Extensions;

namespace GeocachingApi.Domain.Services
{
    public class DataService : IDataService
    {
        private readonly geocachingContext dbContext;

        public DataService(geocachingContext context)
        {
            this.dbContext = context;
        }

        public async Task<IEnumerable<GeocacheModel>> GetGeocaches()
        {
            var geocaches = await GeocachesQueries.GetGeocaches(this.dbContext);

            return geocaches ?? new List<GeocacheModel>();
        }

        public async Task<GeocacheModel> GetGeocache(int id)
        {
            var geocache = await GeocachesQueries.GetGeocache(this.dbContext, id);

            return geocache ?? new GeocacheModel();
        }

        public async Task<bool> GeocacheIdExists(int geocacheId)
        {
            var geocache = await this.GetGeocache(geocacheId);
            return geocache.Id > 0;
        }

        public async Task<IEnumerable<GeocacheItemModel>> GetGeocacheItemsByGeocacheId(int geocacheId, bool activeOnly)
        {
            var geocacheItems = await GeocacheItemsQueries.GetGeocacheItemsByGeocacheId(this.dbContext, geocacheId);

            if (activeOnly)
            {
                geocacheItems = geocacheItems.Where(x => x.IsActive);
            }

            return geocacheItems.ToSafeList();
        }

        /// <summary>
        /// Get geocache item by Id.
        /// </summary>
        /// <param name="id">The id of the Geocache item.</param>
        /// <returns>GeocacheItemModel of the geocache item.</returns>
        public async Task<GeocacheItemModel> GetGeocacheItem(int id)
        {
            var geocacheItem = await GeocacheItemsQueries.GetGeocacheItem(this.dbContext, id);

            return geocacheItem ?? new GeocacheItemModel();
        }

        public async Task<GeocacheItemModel> GetGeocacheItem(string name)
        {
            var geocacheItems = await GeocacheItemsQueries.GetGeocacheItem(this.dbContext, name);

            return geocacheItems ?? new GeocacheItemModel();
        }

        public async Task<GeocacheItemModel> CreateGeocacheItem(IGeocacheItemModel geocacheItem)
        {
            var newGeocacheItem = await GeocacheItemsQueries.CreateGeocacheItem(this.dbContext, geocacheItem);
            geocacheItem = GeocacheItemModelFactory.ConvertFromGeocacheItem(newGeocacheItem);

            return (GeocacheItemModel)geocacheItem;
        }

        public async Task<GeocacheItemModel> UpdateGeocacheItemGeocacheId(int id, int? geocacheId)
        {
            var newGeocacheItem = await GeocacheItemsQueries.UpdateGeocacheItemGeocacheId(this.dbContext, id, geocacheId);
            var geocacheItemModel = GeocacheItemModelFactory.ConvertFromGeocacheItem(newGeocacheItem);

            return (GeocacheItemModel)geocacheItemModel;
        }
    }
}