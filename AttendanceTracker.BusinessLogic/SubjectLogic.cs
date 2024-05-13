using AttendanceTracker.BusinessLogic.Interfaces;
using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;

namespace AttendanceTracker.BusinessLogic
{
    public class SubjectLogic:ISubjectLogic
    {
        ISubjectRepository _subjectRepository;

        public SubjectLogic(ISubjectRepository subjectRepository)
        {
           _subjectRepository = subjectRepository;
        }

        public  async Task<List<Subject>> GetAllSubjects()
        {
            return await _subjectRepository.GetAllSubjects();
        }
    }
}
