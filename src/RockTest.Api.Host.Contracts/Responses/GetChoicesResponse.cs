using Newtonsoft.Json;
using RockTest.Api.Host.Contracts.Dtos;
using System.Text.Json.Serialization;

namespace RockTest.Api.Host.Contracts.Responses;

public class GetChoicesResponse
{
	[JsonProperty("choices")]
	public List<ChoiceDto>? Choices { get; set; }
}
