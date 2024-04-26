using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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
                if (File.Exists(_filePath))
                {
                    records = await ReadLessonsFromFile();
                }
                lesson.LessonId= Guid.NewGuid();
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

        public async Task<List<Lesson>> ReadLessonsFromFile()
        {
            var records = new List<Lesson>();
            if (!File.Exists(_filePath))
            {
                return records;
            }
            string json = await File.ReadAllTextAsync(_filePath);
            if (!json.IsNullOrEmpty())
            {
                records = JsonSerializer.Deserialize<List<Lesson>>(json);
            }
            return records;
        }

        public async Task DeleteLesson(Guid lessonId)
        {
          
            var records = await ReadLessonsFromFile();
            var result = records.RemoveAll(x => x.LessonId == lessonId);
            if (result < 1)
            {
                throw new ArgumentException("No records found");
            }
            var json = JsonSerializer.Serialize(records);
            await File.WriteAllTextAsync(_filePath, json);
        }

        public async Task<List<Lesson>> GetAllLessons()
        {
            return await ReadLessonsFromFile();
        }

        public async Task<Lesson> GetLessonById(Guid lessonId)
        {
           var records= await ReadLessonsFromFile();
           return records.FirstOrDefault(x => x.LessonId == lessonId);
        }

        public async Task UpdateLesson(Lesson lesson)
        {
            var records = await ReadLessonsFromFile();
            var temp = records.FirstOrDefault(x => x.LessonId == lesson.LessonId);
            if(temp==null)
            {
                throw new ArgumentException("No records found");
            }
            temp.SubjectId = lesson.SubjectId;
            temp.ClassRoomId = lesson.ClassRoomId;
            temp.ProfessorId = lesson.ProfessorId;
            var json = JsonSerializer.Serialize(records);
            await File.WriteAllTextAsync(_filePath, json);
        }

        public Task AddFromOtherDb(List<Lesson> lessons)
        {
            throw new NotImplementedException();
        }

        public Task<List<Lesson>> GetUnSyncedData()
        {
            throw new NotImplementedException();
        }

        public Task UpdateSyncFlags(List<Guid> lessonIds)
        {
            throw new NotImplementedException();
        }

        public Task AddFromOtherDb(List<Lesson> lessons, int synced)
        {
            throw new NotImplementedException();
        }
    }
}
