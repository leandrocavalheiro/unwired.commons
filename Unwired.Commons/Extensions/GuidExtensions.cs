namespace Unwired.Commons.Extensions;

public static class GuidExtensions
{
    public static Guid ToGuid(this Guid? value)
    {
        if (value.IsNullOrEmpty())
            return Guid.Empty;

        return (Guid)value;
    }
    public static bool IsNullOrEmpty(this Guid? value)
    {
        if (string.IsNullOrEmpty(value?.ToString()))
            return true;

        return (value == Guid.Empty);
    }
    public static bool IsNullOrEmpty(this Guid value)
    {
        if (string.IsNullOrEmpty(value.ToString()))
            return true;

        return (value == Guid.Empty);
    }        
    public static bool IsValid(this Guid value)    
        => !value.IsNullOrEmpty() && Guid.TryParse(value.ToString(), out _);    
    public static bool IsValid(Guid? value)    
        => !value.IsNullOrEmpty() && Guid.TryParse(value.ToString(), out _);
    
}