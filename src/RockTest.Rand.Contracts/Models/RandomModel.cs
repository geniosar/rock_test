using System.Text.Json.Serialization;

namespace RockTest.Rand.Contracts.Models;

public class RandomModel
{
	[JsonPropertyName("random")]
	public int Value { get; set; }
}
