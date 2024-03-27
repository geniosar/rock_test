using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using RockTest.Rand.Contracts.Interfaces;
using RockTest.Services.Constants;
using RockTest.Core.Extensions;

namespace RockTest.Rand.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddRandomizer(this IServiceCollection self, IConfiguration cfg)
	{
		var retryCount = cfg.GetRequiredSection($"{AppEnvironmentNames.Root}").GetValue<int?>(AppEnvironmentNames.RetryCount);
		var powValue = cfg.GetRequiredSection($"{AppEnvironmentNames.Root}").GetValue<int?>(AppEnvironmentNames.PowValue);
		var baseAddress = cfg.GetRequiredSection($"{AppEnvironmentNames.Root}").GetValue<Uri?>(AppEnvironmentNames.BaseAddress);
		var timeout = cfg.GetRequiredSection($"{AppEnvironmentNames.Root}").GetValue<TimeSpan?>(AppEnvironmentNames.Timeout);

		retryCount.ThrowIfNull(AppEnvironmentNames.RetryCount);
		powValue.ThrowIfNull(AppEnvironmentNames.PowValue);
		baseAddress.ThrowIfNull(AppEnvironmentNames.BaseAddress);
		timeout.ThrowIfNull(AppEnvironmentNames.Timeout);

		var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
			.WaitAndRetryAsync(retryCount!.Value, retryAttempt => TimeSpan.FromSeconds(Math.Pow(powValue!.Value, retryAttempt)));

		// Register the InventoryClient with Polly policies
		self.AddHttpClient<IRandomizer, Randomizer>(AppEnvironmentNames.Root).ConfigureHttpClient(
			(serviceProvider, httpClient) =>
			{
				httpClient.BaseAddress = baseAddress;
				httpClient.Timeout = timeout!.Value;

			}).AddPolicyHandler(retryPolicy);

		return self.AddTransient<IRandomizer, Randomizer>();
	}
}
