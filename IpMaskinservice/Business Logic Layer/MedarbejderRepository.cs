using Data_Access_Layer;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;

namespace Business_Logic_Layer {
    public class MedarbejderRepository : IMedarbejderRepository {
        private readonly MaskinContext _context;

        public MedarbejderRepository(MaskinContext context) {
            _context = context;
        }

        public async Task<Medarbejder?> GetByEmailAsync(string mail) =>
            await _context.Medarbejdere
                .FirstOrDefaultAsync(m => m.MailAdresse == mail);

        public async Task<Medarbejder?> GetByIdAsync(int id) {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Medarbejder medarbejder) {
            await Task.CompletedTask;
        }
    }
}