using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeocachingApi.Infrastructure.Interfaces;
using GeocachingApi.Infrastructure.Models;
using GeocachingApi.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;

namespace GeocachingApi.Domain.Services
{
    public class GeocacheItemsService : IGeocacheItemsService
    {
        private readonly IDataService dataService;
        private readonly ILogger logger;

        public GeocacheItemsService(IDataService dataService, ILogger<GeocacheItemsService> logger)
        {
            this.dataService = dataService;
            this.logger = logger;
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
                throw new ArgumentException("id must be greater than 0.");
            }

            var geocacheItem = await this.dataService.GetGeocacheItem(id);

            return geocacheItem ?? new GeocacheItemModel();
        }

        /// <summary>
        /// Get list of geocache items by given geocache id.
        /// </summary>
        /// <param name="geocacheId">The geocache id.</param>
        /// <param name="activeOnly"><c>true</c> to only include active geocache items; otherwise, <c>false</c> include all results.</param>
        /// <returns>List of GeoCacheItemModel with given geocache id.</returns>
        public async Task<IList<GeocacheItemModel>> GetGeocacheItemsByGeocacheId(int geocacheId, bool activeOnly)
        {
            if (geocacheId <= 0)
            {
                throw new ArgumentException("geocacheId must be greater than 0.");
            }

            var geocacheItems = await this.dataService.GetGeocacheItemsByGeocacheId(geocacheId, activeOnly);

            return geocacheItems.ToSafeList();
        }

        /// <summary>
        /// Create geocache item with give geocache item data.
        /// </summary>
        /// <param name="geocacheItem">The geocache item to create.</param>
        /// <returns>GeocacheItemModel of the created geocache item.</returns>
        public async Task<IGeocacheItemModel> CreateGeocacheItem(IGeocacheItemModel geocacheItem)
        {
            if (geocacheItem == null)
            {
                throw new ArgumentException("geocacheItem cannot be null.");
            }

            geocacheItem = await this.dataService.CreateGeocacheItem(geocacheItem);

            return geocacheItem ?? new GeocacheItemModel();
        }

        /// <summary>
        /// Updates the GeocacheId of the given Geocache item id.
        /// </summary>
        /// <param name="patchModel">The patch model to update the GeocacheId.</param>
        /// <returns>GeocacheItemModel of the updated geocache item.</returns>
        public async Task<IGeocacheItemModel> PatchGeocacheItemGeocacheId(IGeocacheItemPatchGeocacheIdModel patchModel)
        {
            if ((patchModel.Id <= 0) || (patchModel.GeocacheId <= 0))
            {
                throw new ArgumentException("Id's cannot be less than or equal to 0.");
            }

            var geocacheItem = await this.dataService.PatchGeocacheItemGeocacheId(patchModel);

            return geocacheItem ?? new GeocacheItemModel();
        }

        /// <summary>
        /// Validates the geocache item is valid.
        /// </summary>
        /// <param name="geocacheItem">The geocache item to validate.</param>
        /// <returns>List of strings with the collected error messages; if any.</returns>
        public async Task<IList<string>> ValidateGeocacheItem(IGeocacheItemModel geocacheItem)
        {
            var validationMessages = new List<string>();

            if (geocacheItem == null)
            {
                validationMessages.Add("Invalid Geocache Item.");
                return validationMessages;
            }

            if (geocacheItem.ActiveStartDate > geocacheItem.ActiveEndDate)
            {
                validationMessages.Add("Start Date cannot be after End Date.");
            }

            if (geocacheItem.GeocacheId <= 0)
            {
                validationMessages.Add("Invalid GeocacheId.");
            }

            var geocacheItemByName = await this.dataService.GetGeocacheItem(geocacheItem.Name);
            if ((geocacheItemByName?.Id > 0) && (geocacheItemByName.Id != geocacheItem.Id))
            {
                validationMessages.Add("Geocache item name is already in use.");
            }

            if (geocacheItem.GeocacheId > 0)
            {
                if (!await this.dataService.GeocacheIdExists(geocacheItem.GeocacheId ?? 0))
                {
                    validationMessages.Add("Geocache does not exist.");
                }
            }

            return validationMessages;
        }

        /// <summary>
        /// Validates the geocache item is valid for updating the geocache id.
        /// </summary>
        /// <param name="patchModel">The patch model to update the GeocacheId.</param>
        /// <returns>List of strings with the collected error messages; if any.</returns>
        public async Task<IList<string>> ValidateForPatchGeocacheId(IGeocacheItemPatchGeocacheIdModel patchModel)
        {
            var validationMessages = new List<string>();
            var id = patchModel.Id;
            var geocacheId = patchModel.GeocacheId;

            var geocacheItem = await this.GetGeocacheItem(id);
            if (!(geocacheItem.Id > 0))
            {
                validationMessages.Add("Geocache Item does not exist.");
                return validationMessages;
            }

            if (!geocacheItem.IsActive)
            {
                validationMessages.Add("Geocache Item is inactive.");
            }

            if (geocacheId != null)
            {
                if (geocacheId <= 0)
                {
                    validationMessages.Add("Invalid GeocacheId.");
                }
                else
                {
                    var geocacheItemsInGeocache = await GetGeocacheItemsByGeocacheId(geocacheId ?? 0, true);
                    if (geocacheItemsInGeocache.Count >= 3)
                    {
                        validationMessages.Add("Cannot assign to Geocache with 3 or more active items.");
                        return validationMessages;
                    }
                }
            }

            return validationMessages;
        }
    }
}