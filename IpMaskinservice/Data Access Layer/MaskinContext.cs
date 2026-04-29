using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace Data_Access_Layer;

public class MaskinContext : DbContext {
    public MaskinContext(DbContextOptions<MaskinContext> options) : base(options) {
    }

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        if (!optionsBuilder.IsConfigured) {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=IpMaskinDb;Trusted_Connection=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 1. TPT & Tabeller
        modelBuilder.Entity<ServiceOpgave>().UseTptMappingStrategy();

        // 2. Mange-til-mange relationer inkl. seed af bro-tabeller
        modelBuilder.Entity<Service>()
            .HasMany(s => s.OpgaveTypeListe)
            .WithMany()
            .UsingEntity(j => j.ToTable("ServiceOpgaveTypeLinks")
                .HasData(
                    new { ServiceId = 1, OpgaveTypeListeId = 1 },
                    new { ServiceId = 1, OpgaveTypeListeId = 2 }
                ));

      
        modelBuilder.Entity<SikkerhedsEftersyn>()
        .HasMany(s => s.EftersynsRegelListe) // (eller s.EftersynsRegelListe afhængig af dit præcise navn)
        .WithMany()
        .UsingEntity(j => j.ToTable("EftersynsRegelLinks")
            .HasData(
                // RETTET HER: Fra EftersynsRegelId til EftersynsRegelListeId
                new { SikkerhedsEftersynId = 2, EftersynsRegelListeId = 1 }
            ));

        // 3. SEEDING AF DATA

        // Basis Data
        modelBuilder.Entity<Kunde>().HasData(
            new { Id = 1, FirmaNavn = "Byg & Maskin A/S", Adresse = "Testvej 1", CvrNummer = 11223344, ErAktiv = true, KontaktPersonNavn = "Jens Jensen", KontaktPersonTelefonnummer = "12341234", MailAdresse = "B&M@test.com" },
            new { Id = 2, FirmaNavn = "Skovens Entreprenør", Adresse = "Testvej 2", CvrNummer = 99887766, ErAktiv = true, KontaktPersonNavn = "Mia Skov", KontaktPersonTelefonnummer = "43214321", MailAdresse = "Skovens@test.com" }
        );

        modelBuilder.Entity<Medarbejder>().HasData(
            new { Id = "M1", MedarbejderNavn = "Admin Alice", MailAdresse = "admin@ipmaskin.dk" }
        );

        modelBuilder.Entity<ServiceTeknikker>().HasData(
            new { Id = 1, TeknikkerNavn = "Tom Værktøj", TelefonNummer = "11223344" }
        );

        // Typer og Regler
        modelBuilder.Entity<OpgaveType>().HasData(
            new { Id = 1, OpgaveBeskrivelse = "Olieskift" },
            new { Id = 2, OpgaveBeskrivelse = "Udskiftning af hydraulikslange" }
        );

        modelBuilder.Entity<EftersynsRegel>().HasData(
            new { Id = 1, Regel = "Tjek bremser for slid" },
            new { Id = 2, Regel = "Tjek nødstop funktion" }
        );

        modelBuilder.Entity<MaterialeType>().HasData(
            new { Id = 1, MaterialeBeskrivelse = "Motorolie 5W-30" },
            new { Id = 2, MaterialeBeskrivelse = "Hydraulikslange 2m" }
        );

        // Maskiner
        // (EF Core caster automatisk jeres enums, f.eks. MaskineType.Gravemaskine)
        modelBuilder.Entity<Maskine>().HasData(
            new { Id = 1, KundeId = 1, SerieNummer = "SN-1001", Producent = "Volvo", MaskineType = MaskineType.Valsning },
            new { Id = 2, KundeId = 2, SerieNummer = "SN-2002", Producent = "CAT", MaskineType = MaskineType.Pladelaser }
        );

        // Opgaver (TPT: Både base-properties og sub-properties udfyldes her)
        modelBuilder.Entity<Service>().HasData(
            new
            {
                Id = 1,
                MaskineId = 1,
                SidstUdførtDato = new DateOnly(2025, 10, 1),
                Deadline = new DateOnly(2026, 10, 1),
                SidstUdførtNote = "Olie skiftet, alt ok",
                ServiceInterval = ServiceInterval.TOLVMÅNEDER,
                MedarbejderId = "M1",
                ServiceTeknikkerId = 1,
                Servicetype = ServiceType.Fuldservice
            }
        );

        modelBuilder.Entity<SikkerhedsEftersyn>().HasData(
            new
            {
                Id = 2,
                MaskineId = 2,
                SidstUdførtDato = new DateOnly(2025, 11, 1),
                Deadline = new DateOnly(2026, 11, 1),
                SidstUdførtNote = "Nødstop testet og godkendt",
                ServiceInterval = ServiceInterval.TOLVMÅNEDER,
                MedarbejderId = "M1",
                ServiceTeknikkerId = 1
            }
        );

        // Historik og Påmindelser
        modelBuilder.Entity<Påmindelse>().HasData(
            new { Id = 1, PåmindelsesDato = new DateOnly(2026, 9, 1), ServiceOpgaveId = 1 },
            new { Id = 2, PåmindelsesDato = new DateOnly(2026, 10, 1), ServiceOpgaveId = 2 }
        );

        modelBuilder.Entity<AfsluttetService>().HasData(
            new { Id = 1, MaskineId = 1, ServiceOpgaveId = 1, UdførtDato = new DateOnly(2025, 10, 1), Note = "Service afsluttet uden anmærkninger" }
        );
    }
}