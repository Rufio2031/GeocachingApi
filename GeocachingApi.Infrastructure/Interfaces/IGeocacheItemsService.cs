using System.Collections.Generic;
using System.Threading.Tasks;
using GeocachingApi.Infrastructure.Models;

namespace GeocachingApi.Infrastructure.Interfaces
{
    public interface IGeocacheItemsService
    {
        /// <summary>
        /// Get geocache item by Id.
        /// </summary>
        /// <param name="id">The id of the geocache item.</param>
        /// <returns>GeocacheItemModel of the geocache item.</returns>
        Task<GeocacheItemModel> GetGeocacheItem(int id);

        /// <summary>
        /// Get list of geocache items by given geocache id.
        /// </summary>
        /// <param name="geocacheId">The geocache id.</param>
        /// <param name="activeOnly"><c>true</c> to only include active geocache items; otherwise, <c>false</c> include all results.</param>
        /// <returns>List of GeoCacheItemModel with given geocache id.</returns>
        Task<IList<GeocacheItemModel>> GetGeocacheItemsByGeocacheId(int geocacheId, bool activeOnly);

        /// <summary>
        /// Create geocache item with give geocache item data.
        /// </summary>
        /// <param name="geocacheItem">The geocache item to create.</param>
        /// <returns>GeocacheItemModel of the created geocache item.</returns>
        Task<IGeocacheItemModel> CreateGeocacheItem(IGeocacheItemModel geocacheItem);

        /// <summary>
        /// Updates the geocache item of the given id with the given geocache item data.
        /// </summary>
        /// <param name="id">The id of the geocache item to update.</param>
        /// <param name="geocacheId">The geocache data to update with.</param>
        /// <returns>GeocacheItemModel of the updated geocache item.</returns>
        Task<IGeocacheItemModel> UpdateGeocacheItemGeocacheId(int id, int? geocacheId);

        /// <summary>
        /// Validates the geocache item is valid.
        /// </summary>
        /// <param name="geocacheItem">The geocache item to validate.</param>
        /// <returns>List of strings with the collected error messages; if any.</returns>
        Task<IList<string>> ValidateGeocacheItem(IGeocacheItemModel geocacheItem);

        /// <summary>
        /// Validates the geocache item is valid for updating the geocache id.
        /// </summary>
        /// <param name="id">The id of the geocache item to be validated.</param>
        /// <param name="geocacheId">The geocache id of the geocache item to be validated.</param>
        /// <returns>List of strings with the collected error messages; if any.</returns>
        Task<IList<string>> ValidateForUpdateGeocacheId(int id, int? geocacheId);
    }
}