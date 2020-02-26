using Microsoft.EntityFrameworkCore;

namespace ServiceRequest.Api.Data
{
    public class ServiceRequestDbContext : DbContext
    {
        public ServiceRequestDbContext(DbContextOptions options) : base(options) { }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
    }
}
