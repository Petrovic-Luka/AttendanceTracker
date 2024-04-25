using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AttendanceTracker.DataAccess.JSON
{
    public class LessonJSONRepository : ILessonRepository
    {
        private readonly string _filePath;
        private readonly ILogger _logger;
        public LessonJSONRepository(ILogger<LessonJSONRepository> logger)
        {
            _filePath = Environment.CurrentDirectory;
            _filePath += "\\lessons.json";
            _logger = logger;
        }

        public async Task AddLesson(Lesson lesson)
        {
            try
            {
                var records = new List<Lesson>();
                if (!File.Exists(_filePath))
                {
                    File.Create(_filePath);
                }
                else
                {
                    string json = await File.ReadAllTextAsync(_filePath);
                    records = JsonSerializer.Deserialize<List<Lesson>>(json);
                }
                records.Add(lesson);
                var text = JsonSerializer.Serialize(records);
                File.WriteAllText(_filePath, text);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public Task DeleteLesson(Guid lessonId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Lesson>> GetAllLessons()
        {
            string json = File.ReadAllText(_filePath);
            var records = JsonSerializer.Deserialize<List<Lesson>>(json);
            return (List<Lesson>)records;
        }

        public Task<Lesson> GetLessonById(Guid lessonId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateLesson(Lesson lesson)
        {
            throw new NotImplementedException();
        }
    }
}
