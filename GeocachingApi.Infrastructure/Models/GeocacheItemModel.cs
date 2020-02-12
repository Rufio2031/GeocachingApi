using System;
using System.ComponentModel.DataAnnotations;
using GeocachingApi.Infrastructure.Interfaces;

namespace GeocachingApi.Infrastructure.Models
{
    public class GeocacheItemModel : IGeocacheItemModel
    {
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9 ]{1,50}$",
            ErrorMessage = "Characters are not allowed, and must be 50 characters or less.")]
        public string Name { get; set; }
        public int? GeocacheId { get; set; }
        public DateTime ActiveStartDate { get; set; }
        public DateTime ActiveEndDate { get; set; }
        public bool IsActive {
            get {
                var currentDate = DateTime.Now;
                return (currentDate > this.ActiveStartDate) && (currentDate < this.ActiveEndDate);
            }
        }
    }
}
