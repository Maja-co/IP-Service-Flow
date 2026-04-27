namespace Data_Access_Layer.Models;

public interface IMedarbejderRepository
{
    // Metoden der skal returnere et Medarbejder-objekt baseret på mail
    Task<Medarbejder?> GetByEmailAsync(string mail);
    Task<Medarbejder?> GetByIdAsync(int id);
    Task UpdateAsync(Medarbejder medarbejder);
}
