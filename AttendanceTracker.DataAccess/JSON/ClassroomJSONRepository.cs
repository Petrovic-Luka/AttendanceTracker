using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;

namespace AttendanceTracker.DataAccess.JSON
{
    public class ClassroomJSONRepository : IClassroomRepository
    {
        public Task<List<ClassRoom>> GetClassrooms()
        {
            throw new NotImplementedException();
        }
    }
}
