using System.Collections.Generic;
using System.Threading.Tasks;
using GeocachingApi.Infrastructure.Models;

namespace GeocachingApi.Infrastructure.Interfaces
{
    public interface IGeocacheModel
    {
        int Id { get; set; }
        string Name { get; set; }
        int LocationId { get; set; }
    }
}