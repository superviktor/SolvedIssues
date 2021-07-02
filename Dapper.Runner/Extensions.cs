using System.Text.Json;

namespace Dapper.Runner
{
    public static class Extensions
    {
        public static string Serialize<T>(this T input)
        {
            return JsonSerializer.Serialize(input);
        }
    }
}
