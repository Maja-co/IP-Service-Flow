using System.ComponentModel.DataAnnotations;
using Data_Access_Layer;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;

namespace Business_Logic_Layer.Services;

public class ServiceOpgaveService 
{
    private readonly MaskinContext _context;
    
    public ServiceOpgaveService(MaskinContext context)
    {
        _context = context;
    }

    // --- Data til UI Dropdowns ---
    public async Task<List<OpgaveType>> GetOpgaveTyperAsync() => await _context.OpgaveTyper.ToListAsync();
    public async Task<List<EftersynsRegel>> GetEftersynsReglerAsync() => await _context.EftersynsRegler.ToListAsync();
    public List<ServiceInterval> GetServiceIntervaller() => Enum.GetValues(typeof(ServiceInterval)).Cast<ServiceInterval>().ToList();
    public List<ServiceType> GetServiceTyper() => Enum.GetValues(typeof(ServiceType)).Cast<ServiceType>().ToList();
    public async Task<List<Kunde>> GetAlleKunderAsync() => await _context.Kunder.Where(k => k.ErAktiv).ToListAsync();
    public async Task<List<Maskine>> GetMaskinerByKundeIdAsync(int kundeId) => await _context.Maskiner.Where(m => m.KundeId == kundeId).ToListAsync();
    public async Task<List<ServiceTeknikker>> GetAlleTeknikereAsync() => await _context.ServiceTeknikkere.ToListAsync();
    public async Task<List<Medarbejder>> GetAlleMedarbejdereAsync() => await _context.Medarbejdere.ToListAsync();
    
    [Required(ErrorMessage = "Et Firma (Kunde) skal vælges.")]
    public int? ValgtKundeId { get; set; }

    [Required(ErrorMessage = "En maskine skal vælges.")]
    public int? ValgtMaskineId { get; set; }

    [Required(ErrorMessage = "Vælg venligst en kategori (Sikkerhedseftersyn eller Service).")]
    public string ValgtKategori { get; set; } = "";

    public int? ValgtEftersynsRegelId { get; set; }
    public ServiceType? ValgtServiceType { get; set; }
    public int? ValgtOpgaveTypeId { get; set; }
    
    public DateTime Deadline { get; set; } = DateTime.Now.AddMonths(1);

    [Required(ErrorMessage = "Et interval skal vælges.")]
    public ServiceInterval? ValgtInterval { get; set; }

    public int? ValgtTeknikerId { get; set; }

    [Required(ErrorMessage = "En ansvarlig medarbejder skal vælges.")]
    public string? ValgtMedarbejderId { get; set; }

    public string Note { get; set; } = "";
    
    public List<string> Materialer { get; set; } = new();

    public async Task OpretOpgaveFraUIAsync(
        string kategori, 
        int? maskineId, 
        DateTime deadline, 
        ServiceInterval? interval, 
        string note,
        int? teknikerId,
        string? medarbejderId,
        int? eftersynsRegelId,
        ServiceType? serviceType,
        int? opgaveTypeId,
        List<string> materialer)
    {
        // 1. Benhård validering af obligatoriske felter
        if (!maskineId.HasValue) throw new ArgumentException("En maskine skal vælges.");
        if (!interval.HasValue) throw new ArgumentException("Et interval skal vælges.");
        if (string.IsNullOrWhiteSpace(medarbejderId)) throw new ArgumentException("En ansvarlig medarbejder skal vælges.");
        
        var maskine = await _context.Maskiner.FindAsync(maskineId.Value);
        if (maskine == null) throw new Exception("Maskinen findes ikke i databasen.");

        var medarbejder = await _context.Medarbejdere.FindAsync(medarbejderId);
        if (medarbejder == null) throw new Exception("Medarbejderen findes ikke i databasen.");

        // Tekniker må gerne være null
        var tekniker = teknikerId.HasValue ? await _context.ServiceTeknikkere.FindAsync(teknikerId.Value) : null;

        var deadlineDateOnly = DateOnly.FromDateTime(deadline);
        var sidstUdfoert = default(DateOnly);

        ServiceOpgave nyOpgave;

        // 2. Validering af kategori-specifikke felter
        if (kategori == "Sikkerhedseftersyn")
        {
            if (!eftersynsRegelId.HasValue) throw new ArgumentException("En eftersynsregel skal vælges for sikkerhedseftersyn.");

            var regelListe = new List<EftersynsRegel>();
            var regel = await _context.EftersynsRegler.FindAsync(eftersynsRegelId.Value);
            if (regel != null) regelListe.Add(regel);

            nyOpgave = new SikkerhedsEftersyn(maskine, regelListe, sidstUdfoert, deadlineDateOnly, note ?? "", interval.Value, medarbejder, tekniker);
        }
        else if (kategori == "Service")
        {
            if (!serviceType.HasValue) throw new ArgumentException("En servicetype skal vælges.");
            if (!opgaveTypeId.HasValue) throw new ArgumentException("En opgavetype skal vælges.");

            var opgaveTypeListe = new List<OpgaveType>();
            var opgaveTypeObj = await _context.OpgaveTyper.FindAsync(opgaveTypeId.Value);
            if (opgaveTypeObj != null) opgaveTypeListe.Add(opgaveTypeObj);

            nyOpgave = new Service(maskine, serviceType.Value, opgaveTypeListe, sidstUdfoert, deadlineDateOnly, note ?? "", interval.Value, medarbejder, tekniker);
        }
        else
        {
            throw new ArgumentException("Der skal vælges en gyldig kategori (Sikkerhedseftersyn eller Service).");
        }

        // 3. Materialer må gerne være tomme
        if (materialer != null && materialer.Any())
        {
            nyOpgave.MaterialeListe = new MaterialeListe(); 
            // Udvid evt med MaterialeLinje afhængig af din model
        }

        _context.ServiceOpgaver.Add(nyOpgave);
        await _context.SaveChangesAsync();
    }
}