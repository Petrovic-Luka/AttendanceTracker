using AttendanceTracker.Domain;

namespace AttendanceTracker.BusinessLogic.Interfaces
{
    public interface IClassroomLogic
    {
        public Task<List<ClassRoom>> GetClassrooms();
    }
}
