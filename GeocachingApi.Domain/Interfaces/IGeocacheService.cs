﻿using System.Collections.Generic;
using System.Threading.Tasks;
using GeocachingApi.Domain.Models;

namespace GeocachingApi.Domain.Interfaces
{
    public interface IGeocacheService
    {
        Task<IEnumerable<Geocache>> GetActiveGeocaches(int id);
    }
}
