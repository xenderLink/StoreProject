using System.Text.Json;

namespace Store.Infrasctructure;

///<sumary>
/// Расширение для получения и редактирования данных из сессии корзины в формате JSON
///</sumary>
public static class SessionExtension
{
    public static void SetJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonSerializer.Serialize(value)); 
    }

    public static T? GetJson<T>(this ISession session, string key)
    {
        var sessionData = session.GetString(key);
        return sessionData == null ? default(T) : JsonSerializer.Deserialize<T>(sessionData);
    }
}

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