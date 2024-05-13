using AttendanceTracker.BusinessLogic.Interfaces;
using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;

namespace AttendanceTracker.BusinessLogic
{
    public class LessonLogic : ILessonLogic
    {
        private readonly ILessonRepository _repository;

        public LessonLogic(ILessonRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> AddLesson(Lesson lesson)
        {
            return await _repository.AddLesson(lesson);
        }

        public async Task DeleteLesson(Guid lessonId)
        {
            await _repository.DeleteLesson(lessonId);
        }

        public async Task<List<Lesson>> GetAllLessons()
        {
            return await _repository.GetAllLessons();
        }

        public async Task<Lesson> GetLessonById(Guid lessonId)
        {
            return await _repository.GetLessonById(lessonId);
        }

        public async Task UpdateLesson(Lesson lesson)
        {
            await _repository.UpdateLesson(lesson);
        }
    }
}
