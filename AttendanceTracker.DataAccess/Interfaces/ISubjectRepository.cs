using AttendanceTracker.Domain;

namespace AttendanceTracker.DataAccess.Interfaces
{
    public interface ISubjectRepository
    {
        public Task<List<Subject>> GetAllSubjects();

        public Task<Subject> GetSubjectById(int id);
    }
}
