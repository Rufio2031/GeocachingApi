using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GeocachingApi.Domain.DataAccess.Geocaching;
using GeocachingApi.Infrastructure.Interfaces;
using GeocachingApi.Infrastructure;
using GeocachingApi.Infrastructure.Extensions;
using GeocachingApi.Infrastructure.Models;

namespace GeocachingApi.Domain.Queries
{
    class GeocachesQueries
    {
        public static async Task<IEnumerable<GeocacheModel>> GetActiveGeocaches(geocachingContext db)
        {
            return await Task.Run(() => {
                return (from c in db.Geocache
                        select new GeocacheModel
                        {
                            Id = c.Id,
                            Name = c.Name
                        }).ToSafeList();
            });
        }

        public static async Task<GeocacheModel> GetGeocacheById(geocachingContext db, int id)
        {
            return await Task.Run(() => {
                return (from c in db.Geocache
                        where c.Id == id
                        select new GeocacheModel
                        {
                            Id = c.Id,
                            Name = c.Name
                        }).FirstOrDefault();
            });
        }
    }
}