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

        public static async Task<IEnumerable<GeocacheItemModel>> GetGeocacheItemsByGeocacheId(geocachingContext db, int geocacheId)
        {
            return await Task.Run(() => (from ci in db.GeocacheItem
                                         where ci.GeocacheId == geocacheId
                                         select new GeocacheItemModel
                                                {
                                                    Id = ci.Id,
                                                    Name = ci.Name,
                                                    GeocacheId = ci.GeocacheId,
                                                    ActiveStartDate = ci.ActiveStartDate,
                                                    ActiveEndDate = ci.ActiveEndDate
                                                }).ToListAsync());
        }

        public static async Task<GeocacheItemModel> GetGeocacheItem(geocachingContext db, int id)
        {
            return await Task.Run(() => (from ci in db.GeocacheItem
                                         where ci.Id == id
                                         select new GeocacheItemModel
                                                {
                                                    Id = ci.Id,
                                                    Name = ci.Name,
                                                    GeocacheId = ci.GeocacheId,
                                                    ActiveStartDate = ci.ActiveStartDate,
                                                    ActiveEndDate = ci.ActiveEndDate
                                                }).FirstOrDefault());
        }

        public static async Task<GeocacheItemModel> GetGeocacheItem(geocachingContext db, string name)
        {
            return await Task.Run(() => (from ci in db.GeocacheItem
                                         where ci.Name == name
                                         select new GeocacheItemModel
                                                {
                                                    Id = ci.Id,
                                                    Name = ci.Name,
                                                    GeocacheId = ci.GeocacheId,
                                                    ActiveStartDate = ci.ActiveStartDate,
                                                    ActiveEndDate = ci.ActiveEndDate
                                                }).FirstOrDefault());
        }

        public static async Task<GeocacheItem> CreateGeocacheItem(geocachingContext db, IGeocacheItemModel geocacheItem)
        {
            var createdItem = await Task.Run(() =>
                                             {
                                                 var newGeocacheItem = new GeocacheItem
                                                                       {
                                                                           Id = geocacheItem.Id,
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

        public static async Task<GeocacheItem> UpdateGeocacheItemGeocacheId(geocachingContext db, int id, int? geocacheId)
        {
            var createdItem = await Task.Run(() =>
                                             {
                                                 var entityGeocacheItem = db.GeocacheItem.FirstOrDefault(i => i.Id == id);
                                                 if (entityGeocacheItem == null)
                                                 {
                                                     throw new KeyNotFoundException();
                                                 }
                                                 entityGeocacheItem.GeocacheId = geocacheId;
                                                 db.SaveChanges();
                                                 return entityGeocacheItem;
                                             });

            return createdItem;
        }
    }
}