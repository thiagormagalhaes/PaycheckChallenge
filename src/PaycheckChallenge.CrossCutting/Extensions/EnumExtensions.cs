using System.ComponentModel;

namespace PaycheckChallenge.CrossCutting.Extensions;

public static class EnumExtensions
{
    public static string GetDescriptionFromEnumValue(Enum value)
    {
        DescriptionAttribute attribute = value.GetType()
            .GetField(value.ToString())
            .GetCustomAttributes(typeof(DescriptionAttribute), false)
            .SingleOrDefault() as DescriptionAttribute;
        return attribute == null ? value.ToString() : attribute.Description;
    }
}
