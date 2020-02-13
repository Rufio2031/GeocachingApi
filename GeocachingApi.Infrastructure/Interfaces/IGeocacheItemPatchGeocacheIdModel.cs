using System.Collections.Generic;
using System.Threading.Tasks;
using GeocachingApi.Infrastructure.Models;

namespace GeocachingApi.Infrastructure.Interfaces
{
    public interface IGeocacheItemPatchGeocacheIdModel
    {
        int Id { get; set; }
        int? GeocacheId { get; set; }
    }
}