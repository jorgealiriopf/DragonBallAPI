using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DragonBall.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;

namespace DragonBall.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
