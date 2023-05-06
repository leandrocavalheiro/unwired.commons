using System.Text.RegularExpressions;

namespace Unwired.Commons.Extensions;

public static partial class StringExtensions
{
    private static readonly string OnlyNumbersRegex = "[^0-9]";

    [GeneratedRegex("[A-Z]")]
    private static partial Regex SnakeCaseRegex();
    [GeneratedRegex("_[a-z]")]
    private static partial Regex CamelCaseRegex();

    /// <summary>
    /// Convert a string value in SnakeCase format
    /// </summary>
    /// <param name="value">Value to convert</param>
    /// <returns>String. Converted value.</returns>
    public static string ToSnakeCase(this string value)
        => SnakeCaseRegex().Replace(value, "_$0").ToLower();
    /// <summary>
    /// Convert a string value in CamelCase format
    /// </summary>
    /// <param name="value">Value to convert</param>
    /// <returns>String. Converted value.</returns>
    public static string ToCamelCase(this string value)
    {
        return CamelCaseRegex().Replace(value, delegate (Match m) {
            return m.ToString().TrimStart('_').ToUpper();
        });
    }

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
