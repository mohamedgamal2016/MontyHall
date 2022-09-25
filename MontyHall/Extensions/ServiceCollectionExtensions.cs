using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection;
using MontyHall.Core.Common.Commands;
using MontyHall.Core.Factories;
using MontyHall.Core.Services;

namespace MontyHall.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMontyHallCollection(this IServiceCollection services)
        {
            Guard.Against.Null(services, nameof(services));

            services.AddTransient<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<IGameEngineService, GameEngineService>();
            services.AddSingleton<IDoorFactory, DoorFactory>();

            return services;
        }
    }
}
