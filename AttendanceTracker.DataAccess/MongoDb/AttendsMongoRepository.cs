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
            var results = await _attends.FindAsync(x => x.LessonId == attends.LessonId && x.Index==attends.Index);
            if(results.ToList().Count>0)
            {
                throw new ArgumentException("You have already marked your attendance.");
            }
            await _attends.InsertOneAsync(attends);
        }

        public async Task AddFromOtherDb(List<Attends> attends, int synced)
        {
            try
            {
                _session.StartTransaction();
                foreach (var attend in attends)
                {
                    attend.Synced = synced == 1;
                    if(_attends.Count(x=>x.AttendsId==attend.AttendsId)>0)
                    {
                        continue;
                    }

                    await _attends.InsertOneAsync(attend);
                }
                await _session.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await _session.AbortTransactionAsync();
                throw;
            }
        }

        public async Task<List<Attends>> GetAttendsByLesson(Guid lessonId)
        {
            try
            {
                var results = await _attends.FindAsync(x => x.LessonId==lessonId && x.Deleted==false);
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
                var results = await _attends.FindAsync(x => x.Index == index && x.Deleted == false);
                return results.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<Attends>> GetUnSyncedData()
        {
            try
            {
                var results = await _attends.FindAsync(x => x.Synced == false);
                return await results.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task UpdateSyncFlags(List<Guid> attendsIds)
        {
            try
            {
                _session.StartTransaction();
                foreach (var id in attendsIds)
                {
                    var filter = Builders<Attends>.Filter.Eq(x => x.AttendsId, id);
                    var update = Builders<Attends>.Update
                        .Set(x => x.Synced, true);
                    var result = await _attends.UpdateOneAsync(filter, update);
                    if (result.ModifiedCount < 1)
                    {
                        throw new ArgumentException("Record not found");
                    }
                }
                await _session.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await _session.AbortTransactionAsync();
                throw;
            }
        }
    }
}
