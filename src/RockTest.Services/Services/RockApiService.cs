using Microsoft.Extensions.Logging;
using RockTest.Common.Enums;
using RockTest.Games.Contracts.Interfaces;
using RockTest.Services.Contracts.Interfaces;
using RockTest.Services.Contracts.Models;

namespace RockTest.Services.Services;

public class RockApiService(ILogger<RockApiService> logger, IRockGameLogic rockGameLogic) : IRockApiService
{
	public Task<bool> GetChoiceAsync(string userIp, CancellationToken cancellationToken) =>
		rockGameLogic.GetChoiceAsync(userIp, cancellationToken);

	public IEnumerable<RockChoiceEnum> GetChoices() =>
		rockGameLogic.GetChoices();

	public async Task<PlayResultModel> PlayAsync(string userIp, int choiceId, CancellationToken cancellationToken)
	{
		try
		{
			var result = await rockGameLogic.PlayAsync(userIp, choiceId, cancellationToken);
			return new()
			{
				BotChoice = result.BotChoice,
				PlayerChoice = result.PlayerChoice,
				Result = result.Result
			};
		} catch (Exception ex)
		{
			logger.LogError(ex, "Failed to play. UserIp: '{userIp}'. ChoiceId: '{choiceId}'",
				userIp,
				choiceId);

			throw;
		}
	}
}