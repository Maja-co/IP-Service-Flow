namespace Data_Access_Layer.Models;

public interface IMedarbejderRepository
{
    Task<Medarbejder?> GetByEmailAsync(string mail);
    Task<Medarbejder?> GetByIdAsync(int id);
    Task UpdateAsync(Medarbejder medarbejder);
}
