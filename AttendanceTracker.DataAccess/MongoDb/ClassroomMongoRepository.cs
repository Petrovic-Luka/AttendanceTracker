using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace AttendanceTracker.DataAccess.MongoDb
{
    public class ClassroomMongoRepository : IClassroomRepository
    {
        private readonly IMongoCollection<ClassRoom> _classrooms;
        private readonly ILogger _logger;
        private readonly IClientSession _session;

        public ClassroomMongoRepository(MongoDbConnection db, ILogger<ClassroomMongoRepository> logger)
        {
            _classrooms = db.ClassroomCollection;
            _logger = logger;
            _session = db.Session;
        }

        public async Task<ClassRoom> GetClassroomById(int id)
        {
            try
            {
                var results = await _classrooms.FindAsync(x => x.ClassRoomId==id);
                return await results.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<ClassRoom>> GetClassrooms()
        {
            try
            { 
                var results = await _classrooms.FindAsync(_ => true);
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
