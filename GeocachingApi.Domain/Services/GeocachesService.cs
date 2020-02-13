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
        /// <summary>
        /// Gets all geocaches
        /// </summary>
        /// <returns>List of GeocacheModel of all geocaches.</returns>
        public async Task<IList<GeocacheModel>> GetGeocaches()
        {
            var geocaches = await this.dataService.GetGeocaches();

            return geocaches.ToSafeList();
        }

        /// <summary>
        /// Get geocache by id.
        /// </summary>
        /// <param name="id">The id of the geocache.</param>
        /// <returns>GeocacheModel of </returns>
        public async Task<GeocacheModel> GetGeocache(int id)
        {
            var geocache = await this.dataService.GetGeocache(id);

            return geocache ?? new GeocacheModel();
        }
    }
}