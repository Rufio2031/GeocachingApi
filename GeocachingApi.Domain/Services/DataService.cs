using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public async Task<IEnumerable<GeocacheModel>> GetActiveGeocaches()
        {
            var geocaches = await GeocachesQueries.GetActiveGeocaches(this.dbContext);

            return geocaches ?? new List<GeocacheModel>();
        }

        public async Task<GeocacheModel> GetGeocacheById(int id)
        {
            var geocache = await GeocachesQueries.GetGeocacheById(this.dbContext, id);

            return geocache ?? new GeocacheModel();
        }

        public async Task<IEnumerable<GeocacheItemModel>> GetGeocacheItemsByGeocacheId(int id, bool activeOnly)
        {
            var geocacheItems = await GeocacheItemsQueries.GetGeocacheItemsByGeocacheId(this.dbContext, id);

            if (activeOnly)
            {
                geocacheItems = geocacheItems.Where(x => x.IsActive);
            }

            return geocacheItems.ToSafeList();
        }

        public async Task<GeocacheItemModel> CreateGeocacheItem(IGeocacheItemModel geocacheItem)
        {
            var newGeocacheItem = await GeocacheItemsQueries.CreateGeocacheItem(this.dbContext, geocacheItem);
            geocacheItem = GeocacheItemModelFactory.ConvertFromGeocacheItem(newGeocacheItem);

            return (GeocacheItemModel)geocacheItem;
        }

        public async Task<bool> HasUniqueName(string name)
        {
            return await GeocacheItemsQueries.HasUniqueName(this.dbContext, name);
        }

        public async Task<bool> GeocacheIdExists(int geocacheId)
        {
            var geocache = await this.GetGeocacheById(geocacheId);
            return geocache.Id > 0;
        }
    }
}