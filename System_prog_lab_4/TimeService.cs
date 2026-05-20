using Microsoft.AspNetCore.SignalR;

public class TimeService : IHostedService, IDisposable
{
    private readonly IHubContext<ClockHub> _hubContext;
    private System.Timers.Timer _timer; // Явно указываем System.Timers.Timer

    public TimeService(IHubContext<ClockHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new System.Timers.Timer(500); // Явно указываем System.Timers.Timer
        _timer.Elapsed += async (s, e) =>
            await _hubContext.Clients.All.SendAsync("ReceiveTime", DateTime.Now.ToString("HH:mm:ss.fff"));
        _timer.Start();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Stop();
        return Task.CompletedTask;
    }

    public void Dispose() => _timer?.Dispose();
}