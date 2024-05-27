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

        public static void ChangeDbInUse(string value)
        {
            try
            {
                var filePath = "C:\\Users\\lukap\\source\\repos\\AttendanceTracker\\AttendanceTrackerAPI\\appsettings.json";
                string json = File.ReadAllText(filePath);
                var jsonObj = JsonSerializer.Deserialize<AppsettingsWrapper>(json);
                jsonObj.DatabaseInUse = value;
                string output = JsonSerializer.Serialize(jsonObj);
                File.WriteAllText(filePath, output);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}
