using Blog.Models;
using Microsoft.EntityFrameworkCore;
using static Blog.JwtConfigurations;
public class HostedTokenService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public HostedTokenService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var tokens = await context.Tokens.Where(x => x.ExpiredDate.AddHours(Lifetime) < DateTime.UtcNow).ToListAsync(cancellationToken: stoppingToken);
                foreach (var token in tokens)
                {
                    context.Tokens.Remove(token);
                }

                await context.SaveChangesAsync(stoppingToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            await Task.Delay(TimeSpan.FromHours(Lifetime), stoppingToken);
        }
    }

}
