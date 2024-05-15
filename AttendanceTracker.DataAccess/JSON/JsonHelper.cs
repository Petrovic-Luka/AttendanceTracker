using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace AttendanceTracker.DataAccess.JSON
{
    public static class JsonHelper
    {
        public static async Task<List<T>> ReadRecordsFromFile<T>(string _filePath)
        {
            var records = new List<T>();
            if (!File.Exists(_filePath))
            {
                return records;
            }
            string json = await File.ReadAllTextAsync(_filePath);
            if (!json.IsNullOrEmpty())
            {
                records = JsonSerializer.Deserialize<List<T>>(json);
            }
            return records;
        }
    }
}
