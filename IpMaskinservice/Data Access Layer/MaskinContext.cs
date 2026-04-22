using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer;

public class MaskinContext : DbContext
{
    public DbSet<Kunde> Kunder { get; set; }
    public DbSet<Maskine> Maskiner { get; set; }
    public DbSet<ServiceOpgave> ServiceOpgaver { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<SikkerhedsEftersyn> SikkerhedsEftersyn { get; set; }
    public DbSet<Medarbejder> Medarbejdere { get; set; }
    public DbSet<ServiceTeknikker> ServiceTeknikkere { get; set; }
    public DbSet<Påmindelse> Påmindelser { get; set; }
    public DbSet<AfsluttetService> AfsluttedeServices { get; set; }
    public DbSet<OpgaveType> OpgaveTyper { get; set; }
    public DbSet<EftersynsRegel> EftersynsRegler { get; set; }
    public DbSet<MaterialeListe> MaterialeLister { get; set; }
    public DbSet<MaterialeLinje> MaterialeLinjer { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=IpMaskinDb;Trusted_Connection=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TPT Strategi: Den perfekte løsning til arv med relationer
        modelBuilder.Entity<ServiceOpgave>().UseTptMappingStrategy();

        // Mange-til-mange: Service -> OpgaveType (Uden at OpgaveType kender Service)
        modelBuilder.Entity<Service>()
            .HasMany(s => s.OpgaveTypeListe)
            .WithMany() // Tom, da OpgaveType ikke har en liste
            .UsingEntity(j => j.ToTable("ServiceOpgaveTypeLinks"));

        // Mange-til-mange: SikkerhedsEftersyn -> EftersynsRegel (Uden at Regel kender Eftersyn)
        modelBuilder.Entity<SikkerhedsEftersyn>()
            .HasMany(s => s.EftersynsRegelListe)
            .WithMany() // Tom, da EftersynsRegel ikke har en liste
            .UsingEntity(j => j.ToTable("EftersynsRegelLinks"));
    }
}