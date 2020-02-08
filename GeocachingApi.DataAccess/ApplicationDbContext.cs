using System;
using Microsoft.EntityFrameworkCore;
using GeocachingApi.Domain.Models;

namespace GeocachingApi.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<Geocache> Geocache2 { get; set; }
        public virtual DbSet<GeocacheItem> GeocacheItem { get; set; }
    }
}
