using RockTest.Core.Exceptions;

namespace RockTest.Core.Extensions;

public static class ConfigExtension
{
	public static void ThrowIfNull(this int? obj, string key)
	{
		if (obj == null)
		{
			throw new NullValueException(key);
		}
	}

	public static void ThrowIfNull(this TimeSpan? obj, string key)
	{
		if (obj == null)
		{
			throw new NullValueException(key);
		}
	}

	public static void ThrowIfNull(this Uri? obj, string key)
	{
		if (obj == null)
		{
			throw new NullValueException(key);
		}
	}
}
