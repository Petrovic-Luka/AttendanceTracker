using AttendanceTracker.Domain;

namespace AttendanceTracker.BusinessLogic.Interfaces
{
    public interface ILessonLogic
    {
        public Task<Guid> AddLesson(Lesson lesson);
        public Task UpdateLesson(Lesson lesson);
        public Task DeleteLesson(Guid lessonId);
        public Task<Lesson> GetLessonById(Guid lessonId);
        public Task<List<Lesson>> GetAllLessons();
    }
}
