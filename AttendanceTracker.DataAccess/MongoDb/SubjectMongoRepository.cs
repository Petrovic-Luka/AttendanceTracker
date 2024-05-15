using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace AttendanceTracker.DataAccess.MongoDb
{
    public class SubjectMongoRepository : ISubjectRepository
    {

        private readonly IMongoCollection<Subject> _subjects;
        private readonly ILogger _logger;
        private readonly IClientSession _session;

        public SubjectMongoRepository(MongoDbConnection db, ILogger<SubjectMongoRepository> logger)
        {
            _subjects = db.SubjectCollection;
            _logger = logger;
            _session = db.Session;
        }
        public async Task<List<Subject>> GetAllSubjects()
        {
            try
            {
                var results = await _subjects.FindAsync(_ => true);
                return results.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Subject> GetSubjectById(int id)
        {
            try
            {
                var results = await _subjects.FindAsync(x => x.SubjectId==id);
                return await results.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
