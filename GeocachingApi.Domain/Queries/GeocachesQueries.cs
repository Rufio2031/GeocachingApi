using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GeocachingApi.Domain.DataAccess;
using GeocachingApi.Infrastructure.Models;
using GeocachingApi.Infrastructure.Extensions;

namespace GeocachingApi.Domain.Queries
{
    class GeocachesQueries
    {
        public static async Task<IEnumerable<Geocache>> GetActiveGeocaches(ApplicationDbContext db)
        {
            return await Task.Run(() => {
                return (from c in db.Geocache
                        select new Geocache
                        {
                            Id = c.Id,
                            Name = c.Name
                        }).ToSafeList();
            });
        }

        public static async Task<Geocache> GetGeocacheById(ApplicationDbContext db, int id)
        {
            return await Task.Run(() => {
                return (from c in db.Geocache
                        where c.Id == id
                        select new Geocache
                        {
                            Id = c.Id,
                            Name = c.Name
                        }).FirstOrDefault();
            });
        }

        public static async Task<IEnumerable<GeocacheItem>> GetActiveGeocacheItemsByGeocacheId(ApplicationDbContext db, int id)
        {
            return await Task.Run(() => {
                return (from c in db.Geocache
                        join ci in db.GeocacheItem on c.Id equals ci.GeocacheId
                        where c.Id == id
                        && DateTime.Now > ci.ActiveStartDate
                        && DateTime.Now < ci.ActiveEndDate
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
    }
}