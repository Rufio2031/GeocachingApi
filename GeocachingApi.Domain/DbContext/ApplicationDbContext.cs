using System;
using Microsoft.EntityFrameworkCore;
using GeocachingApi.Infrastructure.Models;

namespace GeocachingApi.Domain.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<Geocache> Geocache { get; set; }
        public virtual DbSet<GeocacheItem> GeocacheItem { get; set; }
    }
}