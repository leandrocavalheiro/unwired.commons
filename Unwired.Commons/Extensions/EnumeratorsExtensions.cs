using System.ComponentModel;

namespace Unwired.Commons.Extensions;

public static class EnumeratorsExtensions
{        
    public static string GetDescription<T>(this T enumerationValue) where T : struct
    {
        var type = enumerationValue.GetType();

        //TODO Usar arquivos de resources para mensagem
        if (!type.IsEnum)
            throw new ArgumentException("Argumento deve ser um Enumerador.", nameof(enumerationValue));

        var memberInfo = type.GetMember(enumerationValue.ToString());

        if (memberInfo != null && memberInfo.Length > 0)
        {
            var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs != null && attrs.Length > 0)
                return ((DescriptionAttribute)attrs[0]).Description;
        }

        return enumerationValue.ToString();
    }

    public static string GetDescription<T>(this T? enumerationValue) where T : struct
    {
        if (enumerationValue is null)
            return null;

        return ((T)enumerationValue).GetDescription();       
    }
}
