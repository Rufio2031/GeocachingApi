using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GeocachingApi.Domain.DataAccess.Geocaching;
using GeocachingApi.Infrastructure.Extensions;
using GeocachingApi.Infrastructure.Interfaces;
using GeocachingApi.Infrastructure.Models;

namespace GeocachingApi.Domain.Queries
{
    class GeocacheItemsQueries
    {
        public static async Task<IEnumerable<GeocacheItemModel>> GetGeocacheItemsByGeocacheId(geocachingContext db, int id)
        {
            return await Task.Run(() => {
                                      return (from c in db.Geocache
                                              join ci in db.GeocacheItem on c.Id equals ci.GeocacheId
                                              where (c.Id == id)
                                              select new GeocacheItemModel
                                                     {
                                                         Id = ci.Id,
                                                         Name = ci.Name,
                                                         GeocacheId = ci.GeocacheId,
                                                         ActiveStartDate = ci.ActiveStartDate,
                                                         ActiveEndDate = ci.ActiveEndDate
                                                     }).ToListAsync();
                                  });
        }

        public static async Task<GeocacheItem> CreateGeocacheItem(geocachingContext db, IGeocacheItemModel geocacheItem)
        {
            var createdItem = await Task.Run(() =>
                                             {
                                                 var newGeocacheItem = new GeocacheItem
                                                                       {
                                                                           Name = geocacheItem.Name,
                                                                           GeocacheId = geocacheItem.GeocacheId,
                                                                           ActiveStartDate = geocacheItem.ActiveStartDate,
                                                                           ActiveEndDate = geocacheItem.ActiveEndDate
                                                                       };
                                                 db.GeocacheItem.Add(newGeocacheItem);
                                                 db.SaveChanges();
                                                 return newGeocacheItem;
                                             });

            return createdItem;
        }

        public static bool HasUniqueName(geocachingContext db, string name)
        {
            var result = (from ci in db.GeocacheItem
                         where ci.Name == name
                         select ci).FirstOrDefault();

            return result == null;
        }
    }
}