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

        public async Task<IList<GeocacheItem>> GetGeocacheItemsByGeocacheId(int id, bool activeOnly)
        {
            var geocacheItems = await this.dataService.GetGeocacheItemsByGeocacheId(id, activeOnly);

            return geocacheItems.ToSafeList();
        }

        public async Task<IGeocacheItem> CreateGeocacheItem(IGeocacheItem geocacheItem)
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

        public IList<string> ValidateGeocacheItem(IGeocacheItem geocacheItem)
        {
            var validationMessages = new List<string>();

            if (geocacheItem.ActiveStartDate > geocacheItem.ActiveEndDate)
            {
                validationMessages.Add("Start Date cannot be after End Date.");
            }

            if (geocacheItem.GeocacheId <= 0)
            {
                validationMessages.Add("Invalid GeocacheId.");
            }

            if (!this.dataService.HasUniqueName(geocacheItem.Name))
            {
                validationMessages.Add("Geocache item name is already in use.");
            }

            return validationMessages;
        }
    }
}