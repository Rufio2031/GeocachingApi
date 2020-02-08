using System.Collections.Generic;
using GeocachingApi.Domain.Interfaces;
using GeocachingApi.Domain.Models;

namespace GeocachingApi.Domain.Services
{
    public class GeocacheService : IGeocacheService
    {
        public IEnumerable<Geocache> GetActiveGeocaches()
        {
            return new List<Geocache>();
        }

        public Geocache GetGeocacheById(int id)
        {
            return new Geocache();
        }
    }
}
