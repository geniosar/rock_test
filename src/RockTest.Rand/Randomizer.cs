using Microsoft.Extensions.Logging;
using RockTest.Rand.Contracts.Interfaces;
using RockTest.Rand.Contracts.Models;
using RockTest.Services.Constants;
using System.Text.Json;

namespace RockTest.Rand;

public class Randomizer(ILogger<Randomizer> logger,
	IHttpClientFactory httpClientFactory) : IRandomizer
{

	public async Task<int> GetValueAsync(CancellationToken cancellationToken)
	{
		try
		{
			var client = httpClientFactory.CreateClient(AppEnvironmentNames.Root);
			var response = await client.GetAsync("random", cancellationToken);
			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync(cancellationToken);
				var result = JsonSerializer.Deserialize<RandomModel>(json);
				if (result is not null)
					return result.Value;
			}
		} catch (Exception ex)
		{
			logger.LogError(ex, "Can't get value from third party.");
		}

		return default;
	}
}
