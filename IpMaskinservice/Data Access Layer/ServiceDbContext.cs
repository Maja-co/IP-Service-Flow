using Microsoft.EntityFrameworkCore;
using Data_Access_Layer.Models;

namespace Data_Access_Layer
{
    public class ServiceDbContext : DbContext
    {
        public ServiceDbContext(DbContextOptions<ServiceDbContext> options)
            : base(options)
        {
        }

        // Her tilføjer vi klasserne som tabeller
        public DbSet<AfsluttetService> afsluttetService { get; set; }
        
        public DbSet<Medarbejder> Medarbejder { get; set; }
        public DbSet<Maskine> Maskine { get; set; }
        public DbSet<Kunde> Kunde { get; set; }
        public DbSet<IServiceOpgave> IServiceOpgave { get; set; }
        
    }
}
