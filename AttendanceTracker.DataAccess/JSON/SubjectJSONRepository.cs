using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;

namespace AttendanceTracker.DataAccess.JSON
{
    public class SubjectJSONRepository : ISubjectRepository
    {
        public Task<List<Subject>> GetAllSubjects()
        {
            throw new NotImplementedException();
        }
    }
}
