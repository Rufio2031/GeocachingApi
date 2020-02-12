using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GeocachingApi.Domain.DataAccess;
using GeocachingApi.Infrastructure.Models;
using GeocachingApi.Infrastructure.Extensions;
using GeocachingApi.Infrastructure.Interfaces;

namespace GeocachingApi.Domain.Queries
{
    class GeocacheItemsQueries
    {
        public static async Task<IEnumerable<GeocacheItem>> GetGeocacheItemsByGeocacheId(ApplicationDbContext db, int id)
        {
            return await Task.Run(() => {
                                      return (from c in db.Geocache
                                              join ci in db.GeocacheItem on c.Id equals ci.GeocacheId
                                              where (c.Id == id)
                                              select new GeocacheItem
                                                     {
                                                         Id = ci.Id,
                                                         Name = ci.Name,
                                                         GeocacheId = ci.GeocacheId,
                                                         ActiveStartDate = ci.ActiveStartDate,
                                                         ActiveEndDate = ci.ActiveEndDate
                                                     }).ToListAsync();
                                  });
        }

        public static async Task<GeocacheItem> CreateGeocacheItem(ApplicationDbContext db, IGeocacheItem geocacheItem)
        {
            await Task.Run(() => {
                db.GeocacheItem.Add((GeocacheItem)geocacheItem);
                db.SaveChanges();
            });

            return (GeocacheItem)geocacheItem;
        }

        public static bool HasUniqueName(ApplicationDbContext db, string name)
        {
            var result = (from ci in db.GeocacheItem
                         where ci.Name == name
                         select ci).FirstOrDefault();

            return result == null;
        }
    }
}