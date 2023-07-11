using PoliVagas.Core.Application.NotifyNewJobs;

namespace PoliVagas.Core.Infrastructure.Background;

class NotificationService : BackgroundService
{
    private readonly TimeSpan _period = TimeSpan.FromMinutes(1);
    private readonly ILogger<NotificationService> _logger;
    private readonly IServiceScopeFactory _factory;
    public bool IsEnabled { get; set; } = true;

    public NotificationService(
        ILogger<NotificationService> logger,
        IServiceScopeFactory factory)
    {
        _logger = logger;
        _factory = factory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(_period);
        while (
            !stoppingToken.IsCancellationRequested &&
            await timer.WaitForNextTickAsync(stoppingToken)
        ) {
            try {
                if (IsEnabled) {
                    await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();
                    NotifyNewJobsHandler handler = asyncScope.ServiceProvider.GetRequiredService<NotifyNewJobsHandler>();
                    await handler.Execute();
                    _logger.LogInformation($"Executed NotifyNewJobsHandler");
                }
                else{
                    _logger.LogInformation("Skipped NotifyNewJobsHandler");
                }
            } catch (Exception ex) {
                _logger.LogInformation(
                    $"Failed to execute NotificationService with exception message {ex.Message}.");
            }
        }
    }
}