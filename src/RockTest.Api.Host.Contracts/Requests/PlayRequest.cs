using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace RockTest.Api.Host.Contracts.Requests;

public class PlayRequest
{
	[JsonProperty("player")]
	[JsonPropertyName("player")]
	public int ChoiceId { get; set; }
}
