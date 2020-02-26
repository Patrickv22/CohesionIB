using Microsoft.EntityFrameworkCore;

namespace ServiceRequest.Api.Data
{
    public class ServiceRequestDataContext : DbContext
    {
        public ServiceRequestDataContext(DbContextOptions options) : base(options) { }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
    }
}
