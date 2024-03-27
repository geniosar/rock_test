using Microsoft.Extensions.DependencyInjection;
using RockTest.Games.Contracts.Interfaces;
using RockTest.Games.Games;

namespace RockTest.Games.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddRockGameLogic(this IServiceCollection self)
	{
		self.AddSingleton<IRockResultProcessor, RockResultProcessor>();
		return self.AddTransient<IRockGameLogic, RockGameLogic>();
	}
}
