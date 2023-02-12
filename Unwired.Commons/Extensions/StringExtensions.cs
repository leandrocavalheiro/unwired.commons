using System.Text.RegularExpressions;

namespace Unwired.Commons.Extensions;

public static class StringExtensions
{
    private static readonly string OnlyNumbersRegex = "[^0-9]";
    public static bool Filled(this string value)
        => !string.IsNullOrEmpty(value);    
    public static Guid ToGuid(this string value)
    {
        try
        {
            return Guid.Parse(value);
        }
        catch
        {
            return Guid.Empty;
        }
    }
    public static Guid? ToGuidNullable(this string value)
    {
        if (Guid.TryParse(value, out var newGuid))
            return newGuid;
        return null;
    }
    public static DateTime ToDateTime(this string value, DateTimeKind kind = DateTimeKind.Utc)
    {        
        DateTime result;
        try
        {
            _ = DateTime.TryParse(value, out result);
        }
        catch (Exception)
        {
            result = DateTime.Now;            
        }

        return result.SetKind(kind);
    }
    public static decimal ToDecimal(this string value)
    {
        _ = decimal.TryParse(value, out decimal result);
        return result;
    }
    public static double  ToDouble(this string value)
    {
        _ = double.TryParse(value.Replace(".","").Replace(",","."), out double result);
        return result;
    }
    public static int ToInt(this string value)
    {
        _ = int.TryParse(value, out int result);
        return result;
    }
    public static uint ToUInt(this string value)
    {
        _ = uint.TryParse(value, out uint result);
        return result;
    }
    public static bool IsGuid(this string value)
        => Guid.TryParse(value, out _);   
    public static string OnlyNumber(this string value)    
        => (!string.IsNullOrEmpty(value)) ? Regex.Replace(value, OnlyNumbersRegex, "") : value;
    public static string GetValueOrDefault(this string value, string defaultValue = "")
    {
        if (string.IsNullOrEmpty(value))
            return defaultValue;

        return value;        
    }    
}
