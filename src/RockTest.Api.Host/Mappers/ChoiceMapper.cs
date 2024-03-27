using RockTest.Api.Host.Contracts.Dtos;
using RockTest.Common.Enums;

namespace RockTest.Api.Host.Mappers;

internal static class ChoiceMapper
{
	internal static ChoiceDto ToDto(this RockChoiceEnum choice) =>
		new()
		{
			Name = choice
		};

	internal static List<ChoiceDto> ToDtos(this IEnumerable<RockChoiceEnum> choices) =>
		choices.Select(ToDto).ToList();
}
