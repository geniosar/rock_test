using RockTest.Common.Enums;

namespace RockTest.Games.Contracts.Models;

public class RockResult
{
	public RockGameResultEnum Result { get; set; }
	public RockChoiceEnum PlayerChoice { get; set; }
	public RockChoiceEnum BotChoice { get; set; }
}
