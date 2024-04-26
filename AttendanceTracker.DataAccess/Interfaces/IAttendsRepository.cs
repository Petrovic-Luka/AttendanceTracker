using AttendanceTracker.Domain;

namespace AttendanceTracker.DataAccess.Interfaces
{
    public interface IAttendsRepository
    {
        public Task AddAttends(Attends attends);
        public Task<List<Attends>> GetAttendsByLesson(Guid lessonId);
        public Task<List<Attends>> GetAttendsByStudent(string index);
    }
}
