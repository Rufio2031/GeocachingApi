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
        /// Updates the GeocacheId of the given Geocache item id.
        /// </summary>
        /// <param name="patchModel">The patch model to update the GeocacheId.</param>
        /// <returns>GeocacheItemModel of the updated geocache item.</returns>
        Task<IGeocacheItemModel> PatchGeocacheItemGeocacheId(IGeocacheItemPatchGeocacheIdModel patchModel);

        /// <summary>
        /// Validates the geocache item is valid.
        /// </summary>
        /// <param name="geocacheItem">The geocache item to validate.</param>
        /// <returns>List of strings with the collected error messages; if any.</returns>
        Task<IList<string>> ValidateGeocacheItem(IGeocacheItemModel geocacheItem);

        /// <summary>
        /// Validates the geocache item is valid for updating the geocache id.
        /// </summary>
        /// <param name="patchModel">The patch model to update the GeocacheId.</param>
        /// <returns>List of strings with the collected error messages; if any.</returns>
        Task<IList<string>> ValidateForPatchGeocacheId(IGeocacheItemPatchGeocacheIdModel patchModel);
    }
}