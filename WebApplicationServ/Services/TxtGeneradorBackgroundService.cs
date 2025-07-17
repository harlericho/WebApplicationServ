namespace WebApplicationServ.Services
{
    public class TxtGeneradorBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public TxtGeneradorBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<TxtGeneradorService>();
                    var ruta = Path.Combine(Directory.GetCurrentDirectory(), "Generado1.txt");
                    await service.GenerarTxtAsync(ruta); // genera el archivo
                }

                //await Task.Delay(TimeSpan.FromHours(1), stoppingToken); // espera 1 hora
                //await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken); // espera 30 minutos
                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);// Esperar 2 segundos antes de volver a ejecutar
            }
        }
    }
}
