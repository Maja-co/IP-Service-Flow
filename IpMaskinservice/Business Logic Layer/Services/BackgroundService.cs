using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Business_Logic_Layer.Services {
    public class ServicePåmindelsesWorker : BackgroundService {
        private readonly IServiceScopeFactory _scopeFactory;

        public ServicePåmindelsesWorker(IServiceScopeFactory scopeFactory) {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            while (!stoppingToken.IsCancellationRequested) {
                try {
                    using (var scope = _scopeFactory.CreateScope()) {
                        var påmindelsesService = scope.ServiceProvider.GetRequiredService<PåmindelsesService>();
                        await påmindelsesService.TjekOgSendPåmindelserAsync();
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine($"Fejl i ServicePåmindelsesWorker: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}