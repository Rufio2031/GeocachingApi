using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GeocachingApi.Infrastructure.Interfaces;
using GeocachingApi.Infrastructure.Models;
using GeocachingApi.Domain.DataAccess;
using GeocachingApi.Domain.Queries;
using GeocachingApi.Infrastructure.Extensions;

namespace GeocachingApi.Domain.Services
{
    public class DataService : IDataService
    {
        private readonly ApplicationDbContext dbContext;

        public DataService(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        public async Task<IEnumerable<Geocache>> GetActiveGeocaches()
        {
            var geocaches = await GeocachesQueries.GetActiveGeocaches(this.dbContext);

            return geocaches ?? new List<Geocache>();
        }

        public async Task<Geocache> GetGeocacheById(int id)
        {
            var geocache = await GeocachesQueries.GetGeocacheById(this.dbContext, id);

            return geocache ?? new Geocache();
        }

        public async Task<IEnumerable<GeocacheItem>> GetGeocacheItemsByGeocacheId(int id, bool activeOnly)
        {
            var geocacheItems = await GeocacheItemsQueries.GetGeocacheItemsByGeocacheId(this.dbContext, id);

            if (activeOnly)
            {
                geocacheItems = geocacheItems.Where(x => x.IsActive);
            }

            return geocacheItems.ToSafeList();
        }

        public async Task<GeocacheItem> CreateGeocacheItem(IGeocacheItem geocacheItem)
        {
            geocacheItem = await GeocacheItemsQueries.CreateGeocacheItem(this.dbContext, geocacheItem);

            return (GeocacheItem)geocacheItem;
        }

        public bool HasUniqueName(string name)
        {
            return GeocacheItemsQueries.HasUniqueName(this.dbContext, name);
        }
    }
}