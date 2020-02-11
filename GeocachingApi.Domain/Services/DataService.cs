using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            var geocaches = await GeocachesQueries.GetActiveGeocaches(dbContext);

            return geocaches ?? new List<Geocache>();
        }

        public async Task<Geocache> GetGeocacheById(int id)
        {
            var geocache = await GeocachesQueries.GetGeocacheById(dbContext, id);

            return geocache ?? new Geocache();
        }

        public async Task<IEnumerable<GeocacheItem>> GetActiveGeocacheItemsByGeocacheId(int id)
        {
            var geocacheItems = await GeocachesQueries.GetActiveGeocacheItemsByGeocacheId(dbContext, id);

            return geocacheItems.ToSafeList();
            ;
        }
    }
}