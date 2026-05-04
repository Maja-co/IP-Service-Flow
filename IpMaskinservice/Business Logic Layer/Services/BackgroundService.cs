using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services
{
    public class ServicePåmindelsesWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ServicePåmindelsesWorker(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var påmindelsesService = scope.ServiceProvider.GetRequiredService<PåmindelsesService>();

                        // Her kalder vi metoden, der håndterer alt logikken med at finde opgaver og sende mails
                        await påmindelsesService.TjekOgSendPåmindelserAsync();
                    }
                }
                catch (Exception ex)
                {
                    // Fanger evt. fejl, så worker'en kan overleve og prøve igen efter pausen
                    Console.WriteLine($"Fejl i ServicePåmindelsesWorker: {ex.Message}");
                }

                // Pausen! Lige nu sat til 1 minut, så det er nemt at teste. 
                // Når I er færdige med at teste, kan I ændre det til TimeSpan.FromHours(24)
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}