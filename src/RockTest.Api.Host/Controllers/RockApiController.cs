using Microsoft.AspNetCore.Mvc;
using RockTest.Api.Host.Contracts.Responses;
using RockTest.Api.Host.Mappers;
using RockTest.Services.Contracts.Interfaces;
using RockTest.Core.Extensions;
using RockTest.Api.Host.Contracts.Requests;

namespace RockTest.Api.Host.Controllers;

[ApiController]
[Route("api")]
public class RockApiController(ILogger<RockApiController> logger, IRockApiService service) : ControllerBase
{
	[HttpGet("choices")]
	[ProducesResponseType(typeof(GetChoicesResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
	public IActionResult SaveDataAsync()
	{
		try
		{
			var choices = service.GetChoices();

			if (choices.Any())
			{
				return Ok(new GetChoicesResponse
				{
					Choices = choices.ToDtos()
				});
			}
		} catch (Exception ex)
		{
			logger.LogError(ex, "Failed process get choices request.");
			return BadRequest(ex.Message);
		}

		return BadRequest("Internal server error");
	}

	[HttpGet("choice")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> GetChoiceAsync(CancellationToken cancellationToken)
	{
		try
		{
			var userIp = GetIp();
			var result = await service.GetChoiceAsync(userIp, cancellationToken);
			if (result)
			{
				return Ok();
			}
		} catch (Exception ex)
		{
			logger.LogError(ex, "Failed process get choice request.");
			return BadRequest(ex.Message);
		}

		return BadRequest("Internal server error");
	}

	[HttpPost("play")]
	[ProducesResponseType(typeof(PlayResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> PlayAsync(PlayRequest request, CancellationToken cancellationToken)
	{
		try
		{
			var userIp = GetIp();
			var result = await service.PlayAsync(userIp, request.ChoiceId, cancellationToken);
			return Ok(new PlayResponse()
			{
				BotChoiceId = (int)result.BotChoice,
				PlayerChoiceId = (int)result.PlayerChoice,
				Result = result.Result
			});
		} catch (Exception ex)
		{
			logger.LogError(ex, "Failed process play request. Request: '{@req}'.", request);
			return BadRequest(ex.Message);
		}
	}

	private string GetIp()
	{
		var ipAdd = GetHeaderValueAs<string>("X-Forwarded-For")?.SplitCsv()?.FirstOrDefault();

		// bug: RemoteIpAddress is always null in DNX RC1 Update1
		if (ipAdd.IsNullOrWhitespace() && HttpContext?.Connection?.RemoteIpAddress != null)
			ipAdd = HttpContext.Connection.RemoteIpAddress.ToString();

		if (ipAdd.IsNullOrWhitespace())
			ipAdd = GetHeaderValueAs<string>("REMOTE_ADDR");

		return ipAdd;
	}

	private T? GetHeaderValueAs<T>(string headerName)
	{
		if (HttpContext?.Request?.Headers?.TryGetValue(headerName, out var values) ?? false)
		{
			var rawValues = values.ToString();

			if (!rawValues.IsNullOrWhitespace())
				return (T)Convert.ChangeType(values.ToString(), typeof(T));
		}

		return default;
	}
}