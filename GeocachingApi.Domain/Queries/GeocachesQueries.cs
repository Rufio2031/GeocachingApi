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
        public static async Task<IEnumerable<GeocacheModel>> GetGeocaches(geocachingContext db)
        {
            return await Task.Run(() => {
                return (from c in db.Geocache
                        join l in db.GeocacheLocation on c.LocationId equals l.Id
                        select new GeocacheModel
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Location = new GeocacheLocationModel
                                       {
                                           Id = l.Id,
                                           Latitude = l.Latitude,
                                           Longitude = l.Longitude
                                       }
                        }).ToSafeList();
            });
        }

        public static async Task<GeocacheModel> GetGeocache(geocachingContext db, int id)
        {
            return await Task.Run(() => {
                                      return (from c in db.Geocache
                                              join l in db.GeocacheLocation on c.LocationId equals l.Id
                                              where c.Id == id
                                              select new GeocacheModel
                                                     {
                                                         Id = c.Id,
                                                         Name = c.Name,
                                                         Location = new GeocacheLocationModel
                                                                    {
                                                                        Id = l.Id,
                                                                        Latitude = l.Latitude,
                                                                        Longitude = l.Longitude
                                                                    }
                                                     }).FirstOrDefault();
                                  });
        }
    }
}