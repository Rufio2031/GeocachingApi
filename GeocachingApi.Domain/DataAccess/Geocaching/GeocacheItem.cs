using System;
using System.Collections.Generic;

namespace GeocachingApi.Domain.DataAccess.Geocaching
{
    public partial class GeocacheItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? GeocacheId { get; set; }
        public DateTime ActiveStartDate { get; set; }
        public DateTime ActiveEndDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual Geocache Geocache { get; set; }
    }
}
