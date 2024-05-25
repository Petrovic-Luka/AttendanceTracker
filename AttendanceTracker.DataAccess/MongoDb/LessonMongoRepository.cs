using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace AttendanceTracker.DataAccess.MongoDb
{
    public class LessonMongoRepository : ILessonRepository
    {
        private readonly IMongoCollection<Lesson> _lessons;
        private readonly IMongoCollection<Attends> _attends;
        private readonly ILogger _logger;
        private readonly IClientSession _session;

        public LessonMongoRepository(MongoDbConnection db, ILogger<LessonMongoRepository> logger)
        {
            _lessons = db.LessonCollection;
            _attends = db.AttendsCollection;
            _logger = logger;
            _session = db.Session;
        }

        public async Task AddFromOtherDb(List<Lesson> lessons, int synced)
        {
            try
            {
                _session.StartTransaction();
                foreach (var lesson in lessons)
                {
                    lesson.Synced = synced == 1;
                    if (_lessons.Count(x => x.LessonId == lesson.LessonId)> 0)
                    {
                        continue;
                    }
                        await _lessons.InsertOneAsync(lesson);
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

        public async Task<Guid> AddLesson(Lesson lesson)
        {
            lesson.LessonId = Guid.NewGuid();
            await _lessons.InsertOneAsync(lesson);
            return lesson.LessonId;
        }

        public async Task DeleteLesson(Guid lessonId)
        {
            try
            {
                _session.StartTransaction();
                var lesson = await GetLessonById(lessonId);
                var filter = Builders<Lesson>.Filter.Eq(x => x.LessonId, lesson.LessonId);
                var update = Builders<Lesson>.Update
                    .Set(x => x.ProfessorId, lesson.ProfessorId)
                    .Set(x => x.SubjectId, lesson.SubjectId)
                    .Set(x => x.ClassRoomId, lesson.ClassRoomId)
                    .Set(x => x.Time, lesson.Time)
                    .Set(x => x.Synced, false)
                    .Set(x => x.Deleted, true);
                var result = await _lessons.UpdateOneAsync(filter, update);
                if (result.ModifiedCount < 1)
                {
                    throw new ArgumentException("Record not found");
                }

                //update attends
                var filterAttends = Builders<Attends>.Filter.Eq(x => x.LessonId, lesson.LessonId);
                var attendsList = await _attends.FindAsync(x => x.LessonId == lessonId);
                foreach (var attend in attendsList.ToList())
                {
                    var updateAttend = Builders<Attends>.Update
                        .Set(x => x.Synced, false)
                        .Set(x => x.Deleted, true);
                    await _attends.UpdateManyAsync(filterAttends, updateAttend);
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

        public async Task<List<Lesson>> GetAllLessons()
        {
            try
            {
                var results = await _lessons.FindAsync(_ => true);
                return results.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Lesson> GetLessonById(Guid lessonId)
        {
            try
            {
                var results = await _lessons.FindAsync(x => x.LessonId == lessonId && x.Deleted == false);
                return await results.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<Lesson>> GetUnSyncedData()
        {
            try
            {
                var results = await _lessons.FindAsync(x => x.Synced == false);
                return await results.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task UpdateLesson(Lesson lesson)
        {
            try
            {
                var filter = Builders<Lesson>.Filter.Eq(x => x.LessonId, lesson.LessonId);
                var update = Builders<Lesson>.Update
                    .Set(x => x.ProfessorId, lesson.ProfessorId)
                    .Set(x => x.SubjectId, lesson.SubjectId)
                    .Set(x => x.ClassRoomId, lesson.ClassRoomId)
                    .Set(x => x.Time, lesson.Time)
                    .Set(x => x.Synced, false)
                    .Set(x => x.Deleted, lesson.Deleted);
                var result = await _lessons.UpdateOneAsync(filter, update);
                if (result.ModifiedCount < 1)
                {
                    throw new ArgumentException("Record not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task UpdateSyncFlags(List<Guid> lessonIds)
        {
            try
            {
                _session.StartTransaction();
                foreach (var id in lessonIds)
                {
                    var filter = Builders<Lesson>.Filter.Eq(x => x.LessonId, id);
                    var update = Builders<Lesson>.Update
                        .Set(x => x.Synced, true);
                    var result = await _lessons.UpdateOneAsync(filter, update);
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
