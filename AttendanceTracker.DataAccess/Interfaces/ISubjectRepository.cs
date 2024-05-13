using AttendanceTracker.Domain;

namespace AttendanceTracker.DataAccess.Interfaces
{
    public interface ISubjectRepository
    {
        public Task<List<Subject>> GetAllSubjects();
    }
}
