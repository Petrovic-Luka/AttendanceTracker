using AttendanceTracker.Domain;

namespace AttendanceTracker.DataAccess.Interfaces
{
    public interface ILessonRepository
    {
        public Task AddLesson(Lesson lesson);
        public Task UpdateLesson(Lesson lesson);
        public Task DeleteLesson(Guid lessonId);
        public Task<Lesson> GetLessonById(Guid lessonId);
        public Task<List<Lesson>> GetAllLessons();
    }
}
