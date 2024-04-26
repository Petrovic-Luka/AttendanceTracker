using AttendanceTracker.BusinessLogic.Interfaces;
using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;

namespace AttendanceTracker.BusinessLogic
{
    public class AttendsLogic : IAttendsLogic
    {
        IAttendsRepository _repository;

        public AttendsLogic(IAttendsRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAttends(Attends attends)
        {
            await _repository.AddAttends(attends);
        }

        public async Task<List<Attends>> GetAttendsByLesson(Guid lessonId)
        {
            return await _repository.GetAttendsByLesson(lessonId);
        }

        public async Task<List<Attends>> GetAttendsByStudent(string index)
        {
            return await _repository.GetAttendsByStudent(index);
        }
    }
}
