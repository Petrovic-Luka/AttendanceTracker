using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace AttendanceTracker.DataAccess.MongoDb
{
    public class AttendsMongoRepository : IAttendsRepository
    {
        private readonly IMongoCollection<Attends> _attends;
        private readonly ILogger _logger;
        private readonly IClientSession _session;

        public AttendsMongoRepository(MongoDbConnection db, ILogger<AttendsMongoRepository> logger)
        {
            _attends = db.AttendsCollection;
            _logger = logger;
            _session = db.Session;
        }

        public async Task AddAttends(Attends attends)
        {
            //TODO add validation
            await _attends.InsertOneAsync(attends);
        }

        public async Task<List<Attends>> GetAttendsByLesson(Guid lessonId)
        {
            try
            {
                var results = await _attends.FindAsync(x => x.LessonId==lessonId);
                return results.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<Attends>> GetAttendsByStudent(string index)
        {
            try
            {
                var results = await _attends.FindAsync(x => x.Index == index);
                return results.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
