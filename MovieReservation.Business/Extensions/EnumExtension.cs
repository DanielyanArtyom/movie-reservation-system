using System.Reflection;

namespace MovieReservation.Business.Extensions;

internal static class EnumExtension
{
    internal static string GetDisplayName(this System.Enum enumValue)
    {
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
        
        if (fieldInfo == null)
        {
            return string.Empty;
        }

        var displayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();

        return displayAttribute != null ? displayAttribute.Name : string.Empty;
    }
    
    internal static List<string> GetEnumDisplayNames<T>() where T : System.Enum
    {
        return System.Enum.GetValues(typeof(T))
            .Cast<T>()
            .Select(e => e.GetDisplayName())
            .ToList();
    }
}