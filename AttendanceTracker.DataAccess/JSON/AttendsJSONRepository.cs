using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;

namespace AttendanceTracker.DataAccess.JSON
{
    public class AttendsJSONRepository : IAttendsRepository
    {
        public Task AddAttends(Attends attends)
        {
            throw new NotImplementedException();
        }

        public Task<List<Attends>> GetAttendsByLesson(Guid lessonId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Attends>> GetAttendsByStudent(string index)
        {
            throw new NotImplementedException();
        }
    }
}
