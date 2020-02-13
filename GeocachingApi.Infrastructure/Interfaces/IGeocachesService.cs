using System.Collections.Generic;
using System.Threading.Tasks;
using GeocachingApi.Infrastructure.Models;

namespace GeocachingApi.Infrastructure.Interfaces
{
    public interface IGeocachesService
    {
        /// <summary>
        /// Gets all geocaches
        /// </summary>
        /// <returns>List of GeocacheModel of all geocaches.</returns>
        Task<IList<GeocacheModel>> GetGeocaches();

        /// <summary>
        /// Get geocache by id.
        /// </summary>
        /// <param name="id">The id of the geocache.</param>
        /// <returns>GeocacheModel of </returns>
        Task<GeocacheModel> GetGeocache(int id);
    }
}