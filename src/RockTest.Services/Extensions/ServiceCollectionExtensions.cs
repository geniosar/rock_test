using Microsoft.Extensions.DependencyInjection;
using RockTest.Services.Contracts.Interfaces;
using RockTest.Services.Services;

namespace RockTest.Services.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddRockApiService(this IServiceCollection self) =>
		self.AddTransient<IRockApiService, RockApiService>();
}
