using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeocachingApi.Domain.DataAccess;
using GeocachingApi.Infrastructure.Models;
using GeocachingApi.Infrastructure.Extensions;

namespace GeocachingApi.Domain.Queries
{
    class GeocacheQueries
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
    }
}
