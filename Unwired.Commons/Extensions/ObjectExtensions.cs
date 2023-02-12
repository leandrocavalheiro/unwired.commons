using System.ComponentModel;

namespace Unwired.Commons.Extensions;

public static class ObjectExtensions
{
    public static string GetEnumDescription<T>(this T enumerationValue)
    {
        if (enumerationValue is null)
            return null;

        var type = enumerationValue.GetType();

        //TODO Usar arquivos de resources para mensagem
        if (!type.IsEnum)
            throw new ArgumentException("Argumento deve ser um Enumerador.", nameof(enumerationValue));

        var memberInfo = type.GetMember(enumerationValue.ToString());

        if (memberInfo is not null && memberInfo.Length > 0)
        {
            var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attrs is not null && attrs.Length > 0)
                return ((DescriptionAttribute)attrs[0]).Description;
        }

        return enumerationValue.ToString();
    }
}
