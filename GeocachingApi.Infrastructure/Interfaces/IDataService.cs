using System.Collections.Generic;
using System.Threading.Tasks;
using GeocachingApi.Infrastructure.Models;

namespace GeocachingApi.Infrastructure.Interfaces
{
    public interface IDataService
    {
        /// <summary>
        /// Get all geocaches.
        /// </summary>
        /// <returns>List GeocacheModel of all geocaches.</returns>
        Task<IEnumerable<GeocacheModel>> GetGeocaches();

        /// <summary>
        /// Get geocache by Id.
        /// </summary>
        /// <param name="id">The id of the geocache.</param>
        /// <returns>GeocacheModel of the geocache.</returns>
        Task<GeocacheModel> GetGeocache(int id);

        /// <summary>
        /// Checks if the given geocacheId exists.
        /// </summary>
        /// <param name="geocacheId">The id of the geocache to check.</param>
        /// <returns><c>true</c> if geocache exists; otherwise, <c>false</c>.</returns>
        Task<bool> GeocacheIdExists(int geocacheId);

        /// <summary>
        /// Get geocache items by geocache id.
        /// </summary>
        /// <param name="geocacheId">The geocache id to search for.</param>
        /// <param name="activeOnly"><c>true</c> to only include active geocache items; otherwise, <c>false</c> include all results.</param>
        /// <returns>List GeocacheItemModel of geocache items found by geocache id.</returns>
        Task<IEnumerable<GeocacheItemModel>> GetGeocacheItemsByGeocacheId(int geocacheId, bool activeOnly);

        /// <summary>
        /// Get geocache item by Id.
        /// </summary>
        /// <param name="id">The id of the geocache item.</param>
        /// <returns>GeocacheItemModel of the geocache item.</returns>
        Task<GeocacheItemModel> GetGeocacheItem(int id);

        /// <summary>
        /// Get geocache item by name.
        /// </summary>
        /// <param name="name">The name of the geocache item.</param>
        /// <returns>GeocacheItemModel of the geocache item.</returns>
        Task<GeocacheItemModel> GetGeocacheItem(string name);

        /// <summary>
        /// Create geocache item with given geocache item data.
        /// </summary>
        /// <param name="geocacheItem">The geocache item data.</param>
        /// <returns>GeocacheItemModel of the created geocache item.</returns>
        Task<GeocacheItemModel> CreateGeocacheItem(IGeocacheItemModel geocacheItem);

        /// <summary>
        /// Updates the GeocacheId of the given Geocache item id.
        /// </summary>
        /// <param name="patchModel">The patch model to update the GeocacheId.</param>
        /// <returns>GeocacheItemModel of the updated geocache item.</returns>
        Task<GeocacheItemModel> PatchGeocacheItemGeocacheId(IGeocacheItemPatchGeocacheIdModel patchModel);
    }
}