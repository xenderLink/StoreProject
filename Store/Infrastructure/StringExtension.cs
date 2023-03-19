using System.Text.Json;

namespace Store.Infrasctructure;

///<sumary>
/// Расширение строки для проверки соответствия формата JSON
///</sumary>

public static class StringExtension
{
    public static bool isJson(this string source)
    {
        try
        {
            JsonDocument.Parse(source);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}