using System;
using System.Collections.Generic;

namespace GeocachingApi.Domain.DataAccess.Geocaching
{
    public partial class GeocacheLocation
    {
        public GeocacheLocation()
        {
            Geocache = new HashSet<Geocache>();
        }

        public int Id { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual ICollection<Geocache> Geocache { get; set; }
    }
}
