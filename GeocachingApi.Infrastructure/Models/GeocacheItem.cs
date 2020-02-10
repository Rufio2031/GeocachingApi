using System;
using GeocachingApi.Infrastructure.Interfaces;

namespace GeocachingApi.Infrastructure.Models
{
    public class GeocacheItem : IGeocacheItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? GeocacheId { get; set; }
        public DateTime ActiveStartDate { get; set; }
        public DateTime ActiveEndDate { get; set; }
        public bool IsActive { 
            get {
                var currentDate = DateTime.Now;
                return currentDate > this.ActiveStartDate && currentDate < this.ActiveEndDate; 
            } 
        }
    }
}
