using AttendanceTracker.Domain;

namespace AttendanceTracker.BusinessLogic.Interfaces
{
    public interface ISubjectLogic
    {
        public Task<List<Subject>> GetAllSubjects();
    }
}
