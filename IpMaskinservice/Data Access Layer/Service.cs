using System;
using System.Collections.Generic;
using Data_Access_Layer;

namespace Business_Logic_Layer;

public class Service : ServiceOpgave
{
    public ServiceType? Servicetype { get; set; }
    public List<OpgaveType>? OpgaveTypeListe { get; set; }

    public Service() { }
    public Service(Maskine maskine, ServiceType servicetype, List<OpgaveType> opgaveTypeListe, DateOnly sidstUdførtDato, DateOnly deadline, 
                   string sidstUdførtNote, ServiceInterval serviceInterval, Medarbejder medarbejder, ServiceTeknikker serviceTeknikker) 
        : base(maskine, sidstUdførtDato, deadline, sidstUdførtNote, serviceInterval, medarbejder, serviceTeknikker)
    {
        Servicetype = servicetype;
        OpgaveTypeListe = opgaveTypeListe ?? new List<OpgaveType>();
    }
    public override void afslutOpgave(DateOnly udførtDato, string note)
    {
        //TODO implementer try/catch i UI-laget, så det ikke er nødvendigt at håndtere det her
        if (string.IsNullOrWhiteSpace(note))
        {
            throw new ArgumentException("Der skal tilføjes en note, når en opgave afsluttes.");
        }
        if (udførtDato > DateOnly.FromDateTime(DateTime.Now))
        {
            throw new ArgumentException("Udført dato kan ikke være i fremtiden.");
        }
        SidstUdførtDato = udførtDato;
        SidstUdførtNote = note;

        AfsluttetService afsluttet = new AfsluttetService(udførtDato, note, this);
        Maskine.addAfsluttetService(afsluttet);
    }
    public override void createPåmindelse(DateOnly påmindelsesDato)
    {
        if (påmindelsesDato < DateOnly.FromDateTime(DateTime.Now))
        {
            throw new ArgumentException("Påmindelsesdatoen må ikke være i fortiden.");
        }
        Påmindelse newPåmindelse = new Påmindelse(påmindelsesDato, this);
        PåmindelseListe.Add(newPåmindelse);
    }

    public void addOpgaveType(OpgaveType opgaveType)
    {
        OpgaveTypeListe.Add(opgaveType);
    }
}