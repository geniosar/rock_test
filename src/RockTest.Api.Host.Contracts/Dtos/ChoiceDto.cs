using RockTest.Common.Enums;
using System.Text.Json.Serialization;

namespace RockTest.Api.Host.Contracts.Dtos
{
	public class ChoiceDto
	{
		[JsonPropertyName("name")]
		public RockChoiceEnum Name { get; set; }

		[JsonPropertyName("id")]
		public int Id => (int)Name;
	}
}
