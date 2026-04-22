using Data_Access_Layer;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;

namespace Business_Logic_Layer
{
    public class MedarbejderRepository : IMedarbejderRepository
    {
        private readonly ServiceDbContext _context;

        public MedarbejderRepository(ServiceDbContext context)
        {
            _context = context;
        }

        public async Task<Medarbejder?> GetByEmailAsync(string mail) =>
        
            // Vi bruger LINQ til at finde den første medarbejder, hvor mailen matcher.
            // Hvis ingen findes, returneres 'null'.
             await _context.Medarbejder
                .FirstOrDefaultAsync(m => m.MailAdresse == mail);

        // Find medarbejder på ID
        public async Task<Medarbejder?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        // Opdater medarbejder
        public async Task UpdateAsync(Medarbejder medarbejder)
        {
            await Task.CompletedTask; // En hurtig måde at sige "gør ingenting" i en async metode
        }
    }
    }
