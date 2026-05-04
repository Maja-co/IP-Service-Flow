using Data_Access_Layer.Models;
using Data_Access_Layer;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace Business_Logic_Layer.Services {
    public class PåmindelsesService {
        private readonly MaskinContext _context;

        public PåmindelsesService(MaskinContext context) {
            _context = context;
        }

        public List<Påmindelse> HentAktivePåmindelser() {
            return _context.Påmindelser
                .Where(p => p.ServiceOpgave.Maskine.Kunde.ErAktiv == true)
                .ToList();
        }

        public async Task TjekOgSendPåmindelserAsync() {
            DateOnly datoEnMånedFrem = DateOnly.FromDateTime(DateTime.Today.AddMonths(1));
            DateOnly dagsDato = DateOnly.FromDateTime(DateTime.Today);

            var aktuelleServiceOpgaver = await _context.ServiceOpgaver
                .Include(so => so.Maskine)
                .ThenInclude(m => m.Kunde)
                .Include(so => so.Medarbejder)
                .Include(so => so.PåmindelseListe)
                .Where(so => so.Deadline == datoEnMånedFrem)
                .ToListAsync();

            if (!aktuelleServiceOpgaver.Any()) {
                return;
            }

            bool ændringerGemt = false;

            using (var smtpClient = new SmtpClient("sandbox.smtp.mailtrap.io")) {
                smtpClient.Port = 2525;
                smtpClient.Credentials = new NetworkCredential("eb0985bc856369", "1d16dd5f09b288");
                smtpClient.EnableSsl = true;

                foreach (var opgave in aktuelleServiceOpgaver) {
                    if (string.IsNullOrEmpty(opgave.Medarbejder?.MailAdresse)) continue;

                    bool alleredeSendt = opgave.PåmindelseListe != null &&
                                         opgave.PåmindelseListe.Any(p => p.PåmindelsesDato == dagsDato);

                    if (alleredeSendt) continue;

                    var mailMessage = new MailMessage {
                        From = new MailAddress("servicepaamindelse@outlook.com"),
                        Subject = $"Påmindelse: Service på maskine",
                        Body = $@"
                            Kære {opgave.Medarbejder.MedarbejderNavn},

                            Dette er en automatisk påmindelse.
                            Du har en serviceopgave med deadline om en måned.

                            Kunde: {opgave.Maskine?.Kunde?.FirmaNavn}
                            
                            Maskine Info:
                            Producent: {opgave.Maskine?.Producent}
                            Serienummer: {opgave.Maskine?.SerieNummer}

                            Deadline for service: {opgave.Deadline}
                            ",
                        IsBodyHtml = false
                    };

                    mailMessage.To.Add(opgave.Medarbejder.MailAdresse);

                    try {
                        await smtpClient.SendMailAsync(mailMessage);

                        var nyPåmindelse = new Påmindelse(dagsDato, opgave);
                        _context.Påmindelser.Add(nyPåmindelse);

                        ændringerGemt = true;
                    }
                    catch (Exception ex) {
                        Console.WriteLine(
                            $"Fejl ved afsendelse af mail til {opgave.Medarbejder.MailAdresse}: {ex.Message}");
                    }
                }
            }

            if (ændringerGemt) {
                await _context.SaveChangesAsync();
            }
        }
    }
}