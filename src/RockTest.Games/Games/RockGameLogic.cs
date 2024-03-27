using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using RockTest.Common.Enums;
using RockTest.Games.Contracts.Interfaces;
using RockTest.Games.Contracts.Models;
using RockTest.Rand.Contracts.Interfaces;
using System;

namespace RockTest.Games.Games;

internal class RockGameLogic : IRockGameLogic
{
	private readonly ILogger _logger;
	private readonly IDistributedCache _cache;
	private readonly IRandomizer _randomizer;
	private readonly IRockResultProcessor _rockResultProcessor;

	public RockGameLogic(ILogger<RockGameLogic> logger,
		IDistributedCache cache,
		IRandomizer randomizer,
		IRockResultProcessor rockResultProcessor)
	{
		_logger = logger;
		_cache = cache;
		_randomizer = randomizer;
		_rockResultProcessor = rockResultProcessor;
	}

	public async Task<bool> GetChoiceAsync(string userIp, CancellationToken cancellationToken)
	{
		try
		{
			var value = await _randomizer.GetValueAsync(cancellationToken);
			await _cache.SetAsync(userIp, BitConverter.GetBytes(value), cancellationToken);

			return true;
		} catch (Exception ex)
		{
			_logger.LogError(ex, "Can't create choice for player opponent. UserIp:'{opponent}'",
				userIp);
		}

		return default;
	}

	public IEnumerable<RockChoiceEnum> GetChoices()
	{
		foreach (var choice in Enum.GetValues(typeof(RockChoiceEnum)))
		{
			var value = (RockChoiceEnum)choice;
			if (value == RockChoiceEnum.None)
				continue;
			yield return value;
		}
	}

	public async Task<RockResult> PlayAsync(string userIp, int choiceId, CancellationToken cancellationToken)
	{
		var playerChoice = RockChoiceEnum.None;
		if (!Enum.IsDefined(typeof(RockChoiceEnum), choiceId))
		{
			throw new Exception("User choice is not correct.");
		}
		var randValue = await _cache.GetAsync(userIp, cancellationToken) ?? throw new Exception("Bot didn't do a choice.");
		await _cache.RemoveAsync(userIp, cancellationToken);
		playerChoice = (RockChoiceEnum)choiceId;
		var botChoiceValue = BitConverter.ToInt32(randValue);
		var botChoice = (RockChoiceEnum)(botChoiceValue % ((int)Enum.GetValues(typeof(RockChoiceEnum)).Cast<RockChoiceEnum>().OrderBy(x => x).Last() + 1));
		var result = _rockResultProcessor.GetResult(playerChoice, botChoice);

		return new()
		{
			BotChoice = botChoice,
			PlayerChoice = playerChoice,
			Result = result
		};
	}
}
