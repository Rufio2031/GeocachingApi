using System;
using System.Collections.Generic;
using System.Text;

namespace GeocachingApi.Domain.Models
{
    public class GeocacheItem
    {
        /// <summary>
        /// The identification key for a Geocache item.
        /// </summary>
        /// <value>
        /// The geocache item id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// The name for a Geocache item.
        /// </summary>
        /// <value>
        /// The geocache item name.
        /// </value>
        public string Name { get; set; }
    }
}
