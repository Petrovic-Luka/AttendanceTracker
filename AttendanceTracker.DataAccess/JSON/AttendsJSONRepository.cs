using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace AttendanceTracker.DataAccess.JSON
{
    public class AttendsJSONRepository : IAttendsRepository
    {
        private readonly string _filePath;
        private readonly ILogger _logger;

        public AttendsJSONRepository(ILogger<AttendsJSONRepository> logger)
        {
            _filePath = Environment.CurrentDirectory;
            _filePath += "\\JsonFiles\\attends.json";
            _logger = logger;
        }

        public async Task AddAttends(Attends attends)
        {
            try
            {
                var records = new List<Attends>();
                if (File.Exists(_filePath))
                {
                    records = await JsonHelper.ReadRecordsFromFile<Attends>(_filePath);
                }
                if (records.Where(x => x.LessonId == attends.LessonId && x.Index == attends.Index).Count() > 0)
                {
                    throw new ArgumentException("You have already marked your attendance.");
                }
                attends.AttendsId = Guid.NewGuid();
                records.Add(attends);

                var text = JsonSerializer.Serialize(records);
                File.WriteAllText(_filePath, text);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public Task ClearFile()
        {
            File.WriteAllText(_filePath, "[]");
            return Task.CompletedTask;
        }

        public async Task<List<Attends>> GetAttendsByLesson(Guid lessonId)
        {
            var records = await JsonHelper.ReadRecordsFromFile<Attends>(_filePath);
            return records.Where(x => x.LessonId == lessonId).ToList();
        }

        public async Task<List<Attends>> GetAttendsByStudent(string index)
        {
            var records = await JsonHelper.ReadRecordsFromFile<Attends>(_filePath);
            return records.Where(x => x.Index == index).ToList();
        }

        public async Task<List<Attends>> GetAllAttends()
        {
            return await JsonHelper.ReadRecordsFromFile<Attends>(_filePath);
        }

        public Task<List<Attends>> GetUnSyncedData()
        {
            throw new NotImplementedException();
        }

        public Task AddFromOtherDb(List<Attends> attends, int synced)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSyncFlags(List<Guid> attendsIds)
        {
            throw new NotImplementedException();
        }
    }
}
