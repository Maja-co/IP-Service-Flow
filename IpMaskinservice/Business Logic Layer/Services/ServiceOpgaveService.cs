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

    // --- TILSTAND (STATE) TIL UI ---
    public int? ValgtKundeId { get; set; }
    public int? ValgtMaskineId { get; set; }
    public string ValgtKategori { get; set; } = "";
    public int? ValgtEftersynsRegelId { get; set; }
    public ServiceType? ValgtServiceType { get; set; }
    public int? ValgtOpgaveTypeId { get; set; }
    public DateTime Deadline { get; set; } = DateTime.Now.AddMonths(1);
    public ServiceInterval? ValgtInterval { get; set; }
    public int? ValgtTeknikerId { get; set; }
    public string? ValgtMedarbejderId { get; set; }
    public string Note { get; set; } = "";
    public List<string> Materialer { get; private set; } = new();
    public string NytMateriale { get; set; } = "";

    public bool ErRedigeringAktiv { get; set; } = false;
    private int? NuværendeOpgaveId { get; set; }

    // --- LISTER (Kun private setters så Rider er glad) ---
    public List<Kunde> KunderListe { get; private set; } = new();
    public List<Maskine> MaskineListe { get; private set; } = new();
    public List<EftersynsRegel> EftersynsReglerListe { get; private set; } = new();
    public List<OpgaveType> OpgaveTyperListe { get; private set; } = new();
    public List<ServiceType> ServiceTyper { get; private set; } = new();
    public List<ServiceInterval> ServiceIntervaller { get; private set; } = new();
    public List<ServiceTeknikker> TeknikerListe { get; private set; } = new();
    public List<Medarbejder> MedarbejderListe { get; private set; } = new();

    // --- HENT DATA (Nu private fordi de kun bruges internt i servicen) ---
    private async Task<List<OpgaveType>> GetOpgaveTyperAsync() => await _context.OpgaveTyper.ToListAsync();
    private async Task<List<EftersynsRegel>> GetEftersynsReglerAsync() => await _context.EftersynsRegler.ToListAsync();
    private List<ServiceInterval> GetServiceIntervaller() => Enum.GetValues(typeof(ServiceInterval)).Cast<ServiceInterval>().ToList();
    private List<ServiceType> GetServiceTyper() => Enum.GetValues(typeof(ServiceType)).Cast<ServiceType>().ToList();
    private async Task<List<Kunde>> GetAlleKunderAsync() => await _context.Kunder.Where(k => k.ErAktiv).ToListAsync();
    private async Task<List<Maskine>> GetMaskinerByKundeIdAsync(int kundeId) => await _context.Maskiner.Where(m => m.KundeId == kundeId).ToListAsync();
    private async Task<List<ServiceTeknikker>> GetAlleTeknikereAsync() => await _context.ServiceTeknikkere.ToListAsync();
    private async Task<List<Medarbejder>> GetAlleMedarbejdereAsync() => await _context.Medarbejdere.ToListAsync();

    // --- UI-HANDLINGER (Actions) ---
    public async Task InitializeStateAsync(int? paramKundeId, int? paramMaskineId) 
    {
        KunderListe = await GetAlleKunderAsync();
        EftersynsReglerListe = await GetEftersynsReglerAsync();
        OpgaveTyperListe = await GetOpgaveTyperAsync();
        TeknikerListe = await GetAlleTeknikereAsync();
        MedarbejderListe = await GetAlleMedarbejdereAsync();
        ServiceTyper = GetServiceTyper();
        ServiceIntervaller = GetServiceIntervaller();

        if (paramKundeId.HasValue) 
        {
            await KundeValgtAsync(paramKundeId.Value);
            if (paramMaskineId.HasValue) ValgtMaskineId = paramMaskineId.Value;
        }
    }

    public async Task KundeValgtAsync(int? kundeId) 
    {
        ValgtKundeId = kundeId;
        ValgtMaskineId = null;
        MaskineListe = kundeId.HasValue ? await GetMaskinerByKundeIdAsync(kundeId.Value) : new();
    }

    public void TilføjMateriale() 
    {
        if (!string.IsNullOrWhiteSpace(NytMateriale)) 
        {
            Materialer.Add(NytMateriale.Trim());
            NytMateriale = "";
        }
    }

    public void FjernMateriale(int index) => Materialer.RemoveAt(index);
    public void ToggleRedigering() => ErRedigeringAktiv = !ErRedigeringAktiv;

    public void NulstilState() 
    {
        ValgtKundeId = null; ValgtMaskineId = null; ValgtKategori = "";
        ValgtEftersynsRegelId = null; ValgtServiceType = null; ValgtOpgaveTypeId = null;
        Deadline = DateTime.Now.AddMonths(1); ValgtInterval = null;
        ValgtTeknikerId = null; ValgtMedarbejderId = null; Note = "";
        Materialer.Clear(); NytMateriale = ""; MaskineListe.Clear();
        ErRedigeringAktiv = false; NuværendeOpgaveId = null;
    }

    // --- OPRET METODE ---
    public async Task OpretOpgaveFraUiAsync(
        string kategori, int? maskineId, DateTime deadline, ServiceInterval? interval,
        string note, int? teknikerId, string? medarbejderId, int? eftersynsRegelId,
        ServiceType? serviceType, int? opgaveTypeId, List<string> materialer) 
    {
        if (!maskineId.HasValue) throw new ArgumentException("En maskine skal vælges.");
        if (!interval.HasValue) throw new ArgumentException("Et interval skal vælges.");
        if (string.IsNullOrWhiteSpace(medarbejderId)) throw new ArgumentException("En ansvarlig medarbejder skal vælges.");

        var maskine = await _context.Maskiner.FindAsync(maskineId.Value) ?? throw new Exception("Maskinen findes ikke i databasen.");
        var medarbejder = await _context.Medarbejdere.FindAsync(medarbejderId) ?? throw new Exception("Medarbejderen findes ikke i databasen.");
        
        // Bemærk: Hvis din model kræver non-null for tekniker, skal du håndtere det, 
        // men ud fra din ServiceOpgave-klasse ser det ud til at ServiceTeknikker gerne må være null.
        var tekniker = teknikerId.HasValue ? await _context.ServiceTeknikkere.FindAsync(teknikerId.Value) : null;

        var deadlineDateOnly = DateOnly.FromDateTime(deadline);
        var sidstUdført = default(DateOnly);

        ServiceOpgave nyOpgave;

        if (kategori == "Sikkerhedseftersyn") 
        {
            if (!eftersynsRegelId.HasValue) throw new ArgumentException("En eftersynsregel skal vælges for sikkerhedseftersyn.");

            var regelListe = new List<EftersynsRegel>();
            var regel = await _context.EftersynsRegler.FindAsync(eftersynsRegelId.Value);
            if (regel != null) regelListe.Add(regel);

            nyOpgave = new SikkerhedsEftersyn(maskine, regelListe, sidstUdført, deadlineDateOnly, note, interval.Value, medarbejder, tekniker!);
        }
        else if (kategori == "Service") 
        {
            if (!serviceType.HasValue) throw new ArgumentException("En servicetype skal vælges.");
            if (!opgaveTypeId.HasValue) throw new ArgumentException("En opgavetype skal vælges.");

            var opgaveTypeListe = new List<OpgaveType>();
            var opgaveTypeObj = await _context.OpgaveTyper.FindAsync(opgaveTypeId.Value);
            if (opgaveTypeObj != null) opgaveTypeListe.Add(opgaveTypeObj);

            nyOpgave = new Service(maskine, serviceType.Value, opgaveTypeListe, sidstUdført, deadlineDateOnly, note, interval.Value, medarbejder, tekniker!);
        }
        else 
        {
            throw new ArgumentException("Der skal vælges en gyldig kategori.");
        }

        if (materialer != null && materialer.Any()) 
        {
            nyOpgave.MaterialeListe = new MaterialeListe();
        }

        _context.ServiceOpgaver.Add(nyOpgave);
        await _context.SaveChangesAsync();
    }

    // --- HENT TIL INFO-SIDEN ---
    public async Task HentOpgaveTilInfoAsync(int opgaveId) 
    {
        ErRedigeringAktiv = false;
        NuværendeOpgaveId = opgaveId;

        // Base-include
        var opgave = await _context.ServiceOpgaver
            .Include(o => o.Maskine)
            .Include(o => o.Medarbejder)
            .Include(o => o.ServiceTeknikker)
            .FirstOrDefaultAsync(o => o.Id == opgaveId) ?? throw new Exception("Opgaven findes ikke.");

        ValgtKundeId = opgave.Maskine?.KundeId;

        if (ValgtKundeId.HasValue) 
        {
            KunderListe = await GetAlleKunderAsync();
            MaskineListe = await GetMaskinerByKundeIdAsync(ValgtKundeId.Value);
            TeknikerListe = await GetAlleTeknikereAsync();
            MedarbejderListe = await GetAlleMedarbejdereAsync();
            ServiceIntervaller = GetServiceIntervaller();
        }

        ValgtMaskineId = opgave.Maskine?.Id;
        Deadline = opgave.Deadline.ToDateTime(TimeOnly.MinValue);
        ValgtInterval = opgave.ServiceInterval;
        ValgtTeknikerId = opgave.ServiceTeknikker?.Id;
        ValgtMedarbejderId = opgave.Medarbejder?.Id;
        Note = opgave.SidstUdførtNote ?? "";

        // Tøm materialer før vi evt. fylder den (hvis du implementerer MaterialeListe senere)
        Materialer.Clear(); 

        // Kategori-specifik logik med Explicit Loading for listerne
        if (opgave is SikkerhedsEftersyn eftersyn) 
        {
            ValgtKategori = "Sikkerhedseftersyn";
            EftersynsReglerListe = await GetEftersynsReglerAsync();
            
            // Explicit load af regellisten fra databasen
            await _context.Entry(eftersyn).Collection(e => e.EftersynsRegelListe).LoadAsync();
            ValgtEftersynsRegelId = eftersyn.EftersynsRegelListe.FirstOrDefault()?.Id;
        }
        else if (opgave is Service service) 
        {
            ValgtKategori = "Service";
            ServiceTyper = GetServiceTyper();
            OpgaveTyperListe = await GetOpgaveTyperAsync();
            ValgtServiceType = service.Servicetype;
            
            // Explicit load af opgavetyper fra databasen
            await _context.Entry(service).Collection(e => e.OpgaveTypeListe).LoadAsync();
            ValgtOpgaveTypeId = service.OpgaveTypeListe.FirstOrDefault()?.Id;
        }
    }

    // --- OPDATER METODE ---
    public async Task OpdaterOpgaveFraUiAsync() 
    {
        if (!NuværendeOpgaveId.HasValue) throw new Exception("Ingen opgave valgt.");

        var opgave = await _context.ServiceOpgaver.FindAsync(NuværendeOpgaveId.Value) 
                     ?? throw new Exception("Opgaven findes ikke i databasen.");

        // Opdater gængse felter til at matche domænemodellen
        opgave.Deadline = DateOnly.FromDateTime(Deadline);
        opgave.ServiceInterval = ValgtInterval!.Value; // Vi validerer normalt i UI at interval er sat
        opgave.SidstUdførtNote = Note;

        if (ValgtMedarbejderId != null)
            opgave.Medarbejder = await _context.Medarbejdere.FindAsync(ValgtMedarbejderId);

        if (ValgtTeknikerId.HasValue)
            opgave.ServiceTeknikker = await _context.ServiceTeknikkere.FindAsync(ValgtTeknikerId.Value);

        if (opgave is SikkerhedsEftersyn eftersyn && ValgtKategori == "Sikkerhedseftersyn" && ValgtEftersynsRegelId.HasValue) 
        {
            // Vi skal bruge explicit loading, så vi ikke overskriver en tom liste før vi har hentet den rigtige fra DB
            await _context.Entry(eftersyn).Collection(e => e.EftersynsRegelListe).LoadAsync();
            eftersyn.EftersynsRegelListe.Clear();
            var regel = await _context.EftersynsRegler.FindAsync(ValgtEftersynsRegelId.Value);
            if (regel != null) eftersyn.EftersynsRegelListe.Add(regel);
        }
        else if (opgave is Service service && ValgtKategori == "Service" && ValgtServiceType.HasValue && ValgtOpgaveTypeId.HasValue) 
        {
            service.Servicetype = ValgtServiceType.Value;
            await _context.Entry(service).Collection(e => e.OpgaveTypeListe).LoadAsync();
            service.OpgaveTypeListe.Clear();
            var opgaveTypeObj = await _context.OpgaveTyper.FindAsync(ValgtOpgaveTypeId.Value);
            if (opgaveTypeObj != null) service.OpgaveTypeListe.Add(opgaveTypeObj);
        }

        _context.ServiceOpgaver.Update(opgave);
        await _context.SaveChangesAsync();
    }
}