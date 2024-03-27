using RockTest.Common.Enums;

namespace RockTest.Games.Contracts.Interfaces;

public interface IRockResultProcessor
{
	RockGameResultEnum GetResult(RockChoiceEnum playerChoice, RockChoiceEnum botChoice);
}
