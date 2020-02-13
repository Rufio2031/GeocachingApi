using System;
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

        /// <summary>
        /// Get all geocaches.
        /// </summary>
        /// <returns>List GeocacheModel of all geocaches.</returns>
        public async Task<IEnumerable<GeocacheModel>> GetGeocaches()
        {
            var geocaches = await GeocachesQueries.GetGeocaches(this.dbContext);

            return geocaches ?? new List<GeocacheModel>();
        }

        /// <summary>
        /// Get geocache by Id.
        /// </summary>
        /// <param name="id">The id of the geocache.</param>
        /// <returns>GeocacheModel of the geocache.</returns>
        public async Task<GeocacheModel> GetGeocache(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id in DataService.GetGeocache()");
            }

            var geocache = await GeocachesQueries.GetGeocache(this.dbContext, id);

            return geocache ?? new GeocacheModel();
        }

        /// <summary>
        /// Checks if the given geocacheId exists.
        /// </summary>
        /// <param name="geocacheId">The id of the geocache to check.</param>
        /// <returns><c>true</c> if geocache exists; otherwise, <c>false</c>.</returns>
        public async Task<bool> GeocacheIdExists(int geocacheId)
        {
            if (geocacheId <= 0)
            {
                throw new ArgumentException("Invalid geocacheId in DataService.GeocacheIdExists()");
            }

            var geocache = await this.GetGeocache(geocacheId);
            return geocache.Id > 0;
        }

        /// <summary>
        /// Get geocache items by geocache id.
        /// </summary>
        /// <param name="geocacheId">The geocache id to search for.</param>
        /// <param name="activeOnly"><c>true</c> to only include active geocache items; otherwise, <c>false</c> include all results.</param>
        /// <returns>List GeocacheItemModel of geocache items found by geocache id.</returns>
        public async Task<IEnumerable<GeocacheItemModel>> GetGeocacheItemsByGeocacheId(int geocacheId, bool activeOnly)
        {
            if (geocacheId <= 0)
            {
                throw new ArgumentException("Invalid geocacheId in DataService.GetGeocacheItemsByGeocacheId()");
            }

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
        /// <param name="id">The id of the geocache item.</param>
        /// <returns>GeocacheItemModel of the geocache item.</returns>
        public async Task<GeocacheItemModel> GetGeocacheItem(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id in DataService.GetGeocacheItem(id)");
            }

            var geocacheItem = await GeocacheItemsQueries.GetGeocacheItem(this.dbContext, id);

            return geocacheItem ?? new GeocacheItemModel();
        }

        /// <summary>
        /// Get geocache item by name.
        /// </summary>
        /// <param name="name">The name of the geocache item.</param>
        /// <returns>GeocacheItemModel of the geocache item.</returns>
        public async Task<GeocacheItemModel> GetGeocacheItem(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid name in DataService.GetGeocacheItem(name)");
            }

            var geocacheItems = await GeocacheItemsQueries.GetGeocacheItem(this.dbContext, name);

            return geocacheItems ?? new GeocacheItemModel();
        }

        /// <summary>
        /// Create geocache item with given geocache item data.
        /// </summary>
        /// <param name="geocacheItem">The geocache item data.</param>
        /// <returns>GeocacheItemModel of the created geocache item.</returns>
        public async Task<GeocacheItemModel> CreateGeocacheItem(IGeocacheItemModel geocacheItem)
        {
            if (geocacheItem == null)
            {
                throw new ArgumentException("Invalid geocacheItem in DataService.CreateGeocacheItem()");
            }

            var newGeocacheItem = await GeocacheItemsQueries.CreateGeocacheItem(this.dbContext, geocacheItem);
            geocacheItem = GeocacheItemModelFactory.ConvertFromGeocacheItem(newGeocacheItem);

            return (GeocacheItemModel)geocacheItem;
        }

        /// <summary>
        /// Update the geocache item of the given id with the given geocache item data.
        /// </summary>
        /// <param name="id">The id of the geocache item.</param>
        /// <param name="geocacheId">The geocache item data to update with.</param>
        /// <returns>GeocacheItemModel of the updated geocache item.</returns>
        public async Task<GeocacheItemModel> UpdateGeocacheItemGeocacheId(int id, int? geocacheId)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id in DataService.UpdateGeocacheItemGeocacheId()");
            }

            if (geocacheId <= 0)
            {
                throw new ArgumentException("Invalid geocacheId in DataService.UpdateGeocacheItemGeocacheId()");
            }

            var newGeocacheItem = await GeocacheItemsQueries.UpdateGeocacheItemGeocacheId(this.dbContext, id, geocacheId);
            var geocacheItemModel = GeocacheItemModelFactory.ConvertFromGeocacheItem(newGeocacheItem);

            return (GeocacheItemModel)geocacheItemModel;
        }
    }
}