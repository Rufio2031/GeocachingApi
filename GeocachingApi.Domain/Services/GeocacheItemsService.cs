using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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

        public async Task<GeocacheItemModel> GetGeocacheItem(int id)
        {
            var geocacheItem = await this.dataService.GetGeocacheItem(id);

            return geocacheItem ?? new GeocacheItemModel();
        }

        public async Task<IList<GeocacheItemModel>> GetGeocacheItemsByGeocacheId(int geocacheId, bool activeOnly)
        {
            var geocacheItems = await this.dataService.GetGeocacheItemsByGeocacheId(geocacheId, activeOnly);

            return geocacheItems.ToSafeList();
        }

        public async Task<IGeocacheItemModel> CreateGeocacheItem(IGeocacheItemModel geocacheItem)
        {
            try
            {
                geocacheItem = await this.dataService.CreateGeocacheItem(geocacheItem);
            }
            catch (Exception e)
            {
                this.logger.LogError($"Create GeocacheItem failed. Exception: {e}");
                throw;
            }

            return geocacheItem;
        }

        public async Task<IGeocacheItemModel> UpdateGeocacheItemGeocacheId(int id, int? geocacheId)
        {
            if (id <= 0)
            {
                throw new Exception();
            }

            var geocacheItem = await this.dataService.UpdateGeocacheItemGeocacheId(id, geocacheId);

            return geocacheItem;
        }

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

        public async Task<IList<string>> ValidateForUpdateGeocacheId(int id, int? geocacheId)
        {
            var validationMessages = new List<string>();

            var geocacheItem = await this.GetGeocacheItem(id);
            if (geocacheItem.Id <= 0)
            {
                validationMessages.Add("Geocache Item does not exist.");
            }

            if (!geocacheItem.IsActive)
            {
                validationMessages.Add("Geocache Item is inactive.");
            }

            if (geocacheItem.GeocacheId != null)
            {
                if (geocacheId <= 0)
                {
                    validationMessages.Add("Invalid GeocacheId.");
                }

                var geocacheItemsInGeocache = await GetGeocacheItemsByGeocacheId(geocacheId ?? 0, true);
                if (geocacheItemsInGeocache.Count >= 3)
                {
                    validationMessages.Add("Cannot assign to Geocache with 3 or more active items.");
                    return validationMessages;
                }
            }

            return validationMessages;
        }
    }
}