using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace AttendanceTracker.DataAccess.MongoDb
{
    public class LessonMongoRepository : ILessonRepository
    {
        private readonly IMongoCollection<Lesson> _lessons;
        private readonly ILogger _logger;

        public LessonMongoRepository(MongoDbConnection db, ILogger<LessonMongoRepository> logger)
        {
            _lessons = db.LessonCollection;
            _logger = logger;
        }

        public Task AddLesson(Lesson lesson)
        {
            lesson.LessonId = Guid.NewGuid();
            return _lessons.InsertOneAsync(lesson);
        }

        public async Task DeleteLesson(Guid lessonId)
        {
            var filter = Builders<Lesson>.Filter.Eq(x => x.LessonId, lessonId);
            var result= await _lessons.DeleteOneAsync(filter);
            if(result.DeletedCount <1)
            {
                throw new ArgumentException("Record not found");
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
                var results = await _lessons.FindAsync(x => x.LessonId == lessonId);
                return await results.FirstOrDefaultAsync();
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
                    .Set(x => x.Time, lesson.Time);
                var result = await _lessons.UpdateOneAsync(filter,update);
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
    }
}
