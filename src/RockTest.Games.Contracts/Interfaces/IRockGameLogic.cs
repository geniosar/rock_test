using RockTest.Common.Enums;
using RockTest.Games.Contracts.Models;

namespace RockTest.Games.Contracts.Interfaces;

public interface IRockGameLogic
{
	IEnumerable<RockChoiceEnum> GetChoices();
	Task<bool> GetChoiceAsync(string userIp, CancellationToken cancellationToken);
	Task<RockResult> PlayAsync(string userIp, int choiceId, CancellationToken cancellationToken);
}
