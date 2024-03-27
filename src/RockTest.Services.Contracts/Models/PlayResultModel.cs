using RockTest.Common.Enums;

namespace RockTest.Services.Contracts.Models;

public class PlayResultModel
{
	public RockGameResultEnum Result { get; set; }
	public RockChoiceEnum PlayerChoice { get; set; }
	public RockChoiceEnum BotChoice { get; set; }
}
