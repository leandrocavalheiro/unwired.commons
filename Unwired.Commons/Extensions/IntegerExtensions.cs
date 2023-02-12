namespace Unwired.Commons.Extensions;

public static class IntegerExtensions
{
	public static int ToIntSafe(this int? value, int defaultValue = 0)
	{
		try
		{
			_ = int.TryParse($"{value}", out var result);
			return result;
		}
		catch
		{
			return defaultValue;
		}
	}
	public static bool IsGreatherThanZero(this int? value)
	{
		try
		{
			_ = int.TryParse($"{value}", out var result);
			return result > 0;
		}
		catch
		{
			return false;
		}
	}
	public static bool IsGreatherThanZero(this uint? value)
	{
		try
		{
			_ = uint.TryParse($"{value}", out var result);
			return result > 0;
		}
		catch
		{
			return false;
		}
	}
	public static bool IsGreatherThanZero(this ushort? value)
	{
		try
		{
			_ = ushort.TryParse($"{value}", out var result);
			return result > 0;
		}
		catch
		{
			return false;
		}
	}
	public static bool IsGreatherThanZero(this byte? value)
	{
		try
		{
			_ = byte.TryParse($"{value}", out var result);
			return result > 0;
		}
		catch
		{
			return false;
		}
	}
}
