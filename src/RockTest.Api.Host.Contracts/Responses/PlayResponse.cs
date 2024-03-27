using Newtonsoft.Json;
using RockTest.Common.Enums;

namespace RockTest.Api.Host.Contracts.Responses;

public class PlayResponse
{
	[JsonProperty("result")]
	public RockGameResultEnum Result { get; set; }

	[JsonProperty("player")]
	public int PlayerChoiceId { get; set; }

	[JsonProperty("bot")]
	public int BotChoiceId { get; set; }
}
