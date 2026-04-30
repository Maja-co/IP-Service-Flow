using Data_Access_Layer;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;

namespace Business_Logic_Layer.Services
{
    public class MedarbejderService
    {
        private readonly MaskinContext _context;

        public MedarbejderService(MaskinContext context)
        {
            _context = context;
        }

        public async Task<List<Medarbejder>> HentAlleMedarbejdereMedAftalerAsync()
        {
            return await _context.Medarbejdere
                .Include(m => m.ServiceOpgaveListe)
                    .ThenInclude(s => s.Maskine)
                        .ThenInclude(m => m.Kunde)
                .ToListAsync();
        }

        public async Task SletMedarbejderAsync(string id)
        {
            var medarbejder = await _context.Medarbejdere.FindAsync(id);
            if (medarbejder != null)
            {
                _context.Medarbejdere.Remove(medarbejder);
                await _context.SaveChangesAsync();
            }
        }

        public async Task FlytAnsvarAsync(string fraMedarbejderId, string tilMedarbejderId, List<int> aftaleIds)
        {
            var aftaler = await _context.ServiceOpgaver
                .Where(o => aftaleIds.Contains(o.Id) && o.MedarbejderId == fraMedarbejderId)
                .ToListAsync();

            foreach (var aftale in aftaler)
            {
                aftale.MedarbejderId = tilMedarbejderId;
            }

            await _context.SaveChangesAsync();
        }
    }
}