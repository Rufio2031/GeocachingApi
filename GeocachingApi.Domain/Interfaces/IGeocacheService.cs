using System.Collections.Generic;
using GeocachingApi.Domain.Interfaces;
using GeocachingApi.Domain.Models;

namespace GeocachingApi.Domain.Interfaces
{
    public interface IGeocacheService
    {
        public IEnumerable<Geocache> GetActiveGeocaches();

        public Geocache GetGeocacheById(int id);
    }
}
