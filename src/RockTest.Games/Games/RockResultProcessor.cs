using RockTest.Common.Enums;
using RockTest.Games.Contracts.Interfaces;

namespace RockTest.Games.Games;

public class RockResultProcessor : IRockResultProcessor
{
	private List<string> _winResults { get; set; }

	public RockResultProcessor() => Init();

	private void Init()
	{
		_winResults = [
			GetKey(RockChoiceEnum.Scissors, RockChoiceEnum.Paper),
			GetKey(RockChoiceEnum.Paper, RockChoiceEnum.Rock),
			GetKey(RockChoiceEnum.Rock, RockChoiceEnum.Lizard),
			GetKey(RockChoiceEnum.Lizard, RockChoiceEnum.Spock),
			GetKey(RockChoiceEnum.Spock, RockChoiceEnum.Scissors),
			GetKey(RockChoiceEnum.Scissors, RockChoiceEnum.Lizard),
			GetKey(RockChoiceEnum.Lizard, RockChoiceEnum.Paper),
			GetKey(RockChoiceEnum.Paper, RockChoiceEnum.Spock),
			GetKey(RockChoiceEnum.Spock, RockChoiceEnum.Rock),
			GetKey(RockChoiceEnum.Rock, RockChoiceEnum.Scissors)
		];
	}

	public RockGameResultEnum GetResult(RockChoiceEnum playerChoice, RockChoiceEnum botChoice)
	{
		if (playerChoice == botChoice)
			return RockGameResultEnum.Tie;

		if (_winResults.Contains(GetKey(playerChoice, botChoice)))
			return RockGameResultEnum.Win;

		return RockGameResultEnum.Lose;
	}

	private string GetKey(RockChoiceEnum playerChoice, RockChoiceEnum botChoice) =>
		$"{playerChoice}_{botChoice}";
}
