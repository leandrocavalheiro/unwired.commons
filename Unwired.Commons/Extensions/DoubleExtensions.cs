using Unwired.Commons.Enumarators;
using Unwired.Models.ViewModels;

namespace Unwired.Commons.Extensions;

public static class DoubleExtensions
{
    public static double URound(this double value, RoundType roundType = RoundType.Round, int precision = 2, double onErrorReturn = -1)
    {
        double result;
        try
        {
            switch (roundType)
            {
                case RoundType.Floor:
                    result = Math.Floor(value);
                    break;

                case RoundType.Ceiling:
                    result = Math.Ceiling(value);
                    break;

                default:
                    result = Math.Round(value, precision);
                    break;
            }
        }
        catch
        {
            result = onErrorReturn;
        }

        return result;
    }
    public static short ToShort(this double value, RoundType roundType = RoundType.Round, short onErrorReturn = -1)
    {
        _ = double.TryParse(onErrorReturn.ToString(), out var defaultValue);
        return (short)value.URound(roundType, 0, defaultValue);
    }
    public static int ToInt(this double value, RoundType roundType = RoundType.Round, int onErrorReturn = -1)
    {
        _ = double.TryParse(onErrorReturn.ToString(), out var defaultValue);
        return (int)value.URound(roundType, 0, defaultValue);
    }
    public static long ToLong(this double value, RoundType roundType = RoundType.Round, long onErrorReturn = -1)
    {
        _ = double.TryParse(onErrorReturn.ToString(), out var defaultValue);
        return (long)value.URound(roundType, 0, defaultValue);
    }
    public static ushort ToUShort(this double value, RoundType roundType = RoundType.Round, ushort onErrorReturn = 0)
    {
        _ = double.TryParse(onErrorReturn.ToString(), out var defaultValue);
        return (ushort)value.URound(roundType, 0, defaultValue);
    }
    public static uint ToUInt(this double value, RoundType roundType = RoundType.Round, uint onErrorReturn = 0)
    {
        _ = double.TryParse(onErrorReturn.ToString(), out var defaultValue);
        return (uint)value.URound(roundType, 0, defaultValue);
    }
    public static ulong ToULong(this double value, RoundType roundType = RoundType.Round, uint onErrorReturn = 0)
    {
        _ = double.TryParse(onErrorReturn.ToString(), out var defaultValue);
        return (ulong)value.URound(roundType, 0, defaultValue);
    }
    public static decimal ToDecimal(this double value, RoundType roundType = RoundType.Round, int precision = 2, decimal onErrorReturn = -1)
    {
        _ = double.TryParse(onErrorReturn.ToString(), out var defaultValue);
        return (decimal)value.URound(roundType, precision, defaultValue);
    }
    public static float ToFloat(this double value, RoundType roundType = RoundType.Round, int precision = 2, float onErrorReturn = -1)
    {
        _ = double.TryParse(onErrorReturn.ToString(), out var defaultValue);
        return (float)value.URound(roundType, precision, defaultValue);
    }
    public static TResult ToGenericType<TResult>(this double value, RoundType roundType = RoundType.Round, int precision = 2)
    {
        switch (typeof(TResult).ToString())
        {
            case "System.Int16":
                return (TResult)(object)value.ToShort(roundType);
            case "System.Int32":
                return (TResult)(object)value.ToInt(roundType);
            case "System.Int64":
                return (TResult)(object)value.ToLong(roundType);
            case "System.UInt16":
                return (TResult)(object)value.ToUShort(roundType);
            case "System.UInt32":
                return (TResult)(object)value.ToUInt(roundType);
            case "System.UInt64":
                return (TResult)(object)value.ToULong(roundType);
            case "System.Single":
                return (TResult)(object)value.ToFloat(roundType, precision);
            case "System.Decimal":
                return (TResult)(object)value.ToDecimal(roundType, precision);
            default:
                return (TResult)(object)value.URound(roundType, precision);

        }
    }
}
