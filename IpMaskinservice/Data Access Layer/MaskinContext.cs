using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer
{
    public class MaskinContext : DbContext
    {
        public DbSet<Kunde> Kunder { get; set; }
        public DbSet<Maskine> Maskiner { get; set; }
        public DbSet<Medarbejder> Medarbejdere { get; set; }
        public DbSet<ServiceTeknikker> ServiceTeknikkere { get; set; }

        // Service og Opgaver (Arv/TPH)
        public DbSet<ServiceOpgave> ServiceOpgaver { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SikkerhedsEftersyn> SikkerhedsEftersyn { get; set; }

        // Historik og planlægning
        public DbSet<AfsluttetService> AfsluttedeServices { get; set; }
        public DbSet<Påmindelse> Påmindelser { get; set; }

        // Materialer
        public DbSet<MaterialeListe> MaterialeLister { get; set; }
        public DbSet<MaterialeLinje> MaterialeLinjer { get; set; }

        // Typer, Regler og Intervaller
        public DbSet<OpgaveType> OpgaveTyper { get; set; }
        public DbSet<EftersynsRegel> EftersynsRegler { get; set; }
        public DbSet<MaterialeType> MaterialeTyper { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=IpMaskinDb;Trusted_Connection=True;");
            }
        }
    }
}