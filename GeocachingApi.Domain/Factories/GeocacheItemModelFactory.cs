using GeocachingApi.Domain.DataAccess.Geocaching;
using GeocachingApi.Infrastructure.Interfaces;
using GeocachingApi.Infrastructure.Models;

namespace GeocachingApi.Domain.Factories
{
    public static class GeocacheItemModelFactory
    {
        public static IGeocacheItemModel ConvertFromGeocacheItem(GeocacheItem geocacheItem)
        {
            var newGeocacheItem = new GeocacheItemModel();
            newGeocacheItem.Id = geocacheItem.Id;
            newGeocacheItem.Name = geocacheItem.Name;
            newGeocacheItem.GeocacheId = geocacheItem.GeocacheId;
            newGeocacheItem.ActiveStartDate = geocacheItem.ActiveStartDate;
            newGeocacheItem.ActiveEndDate = geocacheItem.ActiveEndDate;

            return newGeocacheItem;
        }
    }
}
