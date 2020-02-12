using System;
using System.Collections.Generic;

namespace GeocachingApi.Domain.DataAccess.Geocaching
{
    public partial class Geocache
    {
        public Geocache()
        {
            GeocacheItem = new HashSet<GeocacheItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual GeocacheLocation Location { get; set; }
        public virtual ICollection<GeocacheItem> GeocacheItem { get; set; }
    }
}
