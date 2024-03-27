using RockTest.Common.Enums;
using RockTest.Services.Contracts.Models;

namespace RockTest.Services.Contracts.Interfaces;

public interface IRockApiService
{
	Task<bool> GetChoiceAsync(string userIp, CancellationToken cancellationToken);
	IEnumerable<RockChoiceEnum> GetChoices();
	Task<PlayResultModel> PlayAsync(string userIp, int choiceId, CancellationToken cancellationToken);
}
