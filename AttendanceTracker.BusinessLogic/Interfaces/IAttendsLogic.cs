using AttendanceTracker.Domain;

namespace AttendanceTracker.BusinessLogic.Interfaces
{
    public interface IAttendsLogic
    {
        public Task AddAttends(Attends attends);
        public Task<List<Attends>> GetAttendsByLesson(Guid lessonId);
        public Task<List<Attends>> GetAttendsByStudent(string index);
    }
}
