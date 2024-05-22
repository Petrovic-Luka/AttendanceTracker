using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AttendanceTracker.DataAccess.JSON
{
    public class LessonJSONRepository : ILessonRepository
    {
        private readonly string _filePathLesson;
        private readonly string _filePathAttends;
        private readonly ILogger _logger;
        public LessonJSONRepository(ILogger<LessonJSONRepository> logger)
        {
            _filePathLesson = Environment.CurrentDirectory;
            _filePathAttends = Environment.CurrentDirectory;
            _filePathLesson += "\\JsonFiles\\lessons.json";
            _filePathAttends+= "\\JsonFiles\\attends.json";
            _logger = logger;
        }

        public async Task<Guid> AddLesson(Lesson lesson)
        {
            try
            {
                var records = new List<Lesson>();
                if (File.Exists(_filePathLesson))
                {
                    records = await JsonHelper.ReadRecordsFromFile<Lesson>(_filePathLesson);
                }
                lesson.LessonId= Guid.NewGuid();
                records.Add(lesson);
                var text = JsonSerializer.Serialize(records);
                File.WriteAllText(_filePathLesson, text);

                return lesson.LessonId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task DeleteLesson(Guid lessonId)
        {

            var lessonRecords = await JsonHelper.ReadRecordsFromFile<Lesson>(_filePathLesson);
            var attendsRecords = await JsonHelper.ReadRecordsFromFile<Attends>(_filePathAttends);
            try
            {
                //remove from lesson
                var count=lessonRecords.RemoveAll(x => x.LessonId == lessonId);
                if (count < 1)
                {
                    throw new ArgumentException("No records found");
                }
                var json = JsonSerializer.Serialize(lessonRecords);
                await File.WriteAllTextAsync(_filePathLesson, json);

                //'cascade' remove from attends
                attendsRecords.RemoveAll(x => x.LessonId == lessonId);
                var jsonAttends = JsonSerializer.Serialize(attendsRecords);
                await File.WriteAllTextAsync(_filePathAttends, jsonAttends);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<Lesson>> GetAllLessons()
        {
            return await JsonHelper.ReadRecordsFromFile<Lesson>(_filePathLesson);
        }

        public async Task<Lesson> GetLessonById(Guid lessonId)
        {
           var records= await JsonHelper.ReadRecordsFromFile<Lesson>(_filePathLesson);
            return records.FirstOrDefault(x => x.LessonId == lessonId);
        }

        public async Task UpdateLesson(Lesson lesson)
        {
            var records = await JsonHelper.ReadRecordsFromFile<Lesson>(_filePathLesson);
            var temp = records.FirstOrDefault(x => x.LessonId == lesson.LessonId);
            if(temp==null)
            {
                throw new ArgumentException("No records found");
            }
            temp.SubjectId = lesson.SubjectId;
            temp.ClassRoomId = lesson.ClassRoomId;
            temp.ProfessorId = lesson.ProfessorId;
            var json = JsonSerializer.Serialize(records);
            await File.WriteAllTextAsync(_filePathLesson, json);
        }

        public Task ClearFile()
        {
            File.WriteAllText(_filePathLesson, "[]");
            return Task.CompletedTask;
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
