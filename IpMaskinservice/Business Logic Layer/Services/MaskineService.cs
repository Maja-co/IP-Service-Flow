using Data_Access_Layer;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;

namespace Business_Logic_Layer.Services {
    public class MaskineService {
        private readonly MaskinContext _context;

        public MaskineService(MaskinContext context) {
            _context = context;
        }

        public async Task<Maskine?> GetMaskineMedHistorikAsync(int id) {
            return await _context.Maskiner
                .Include(m => m.Kunde)
                .Include(m => m.ServiceHistorikListe)
                .ThenInclude(h => h.ServiceOpgave)
                .ThenInclude(o => o.ServiceTeknikker)
                .Include(m => m.ServiceOgEftersynAftalerListe)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task SletMaskineAsync(int id) {
            var maskine = await _context.Maskiner.FindAsync(id);
            if (maskine != null) {
                _context.Maskiner.Remove(maskine);
                await _context.SaveChangesAsync();
            }
        }
    }
}