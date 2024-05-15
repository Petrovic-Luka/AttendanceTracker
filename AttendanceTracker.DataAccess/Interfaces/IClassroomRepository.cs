using AttendanceTracker.Domain;

namespace AttendanceTracker.DataAccess.Interfaces
{
    public interface IClassroomRepository
    {
        public Task<List<ClassRoom>> GetClassrooms();
        public Task<ClassRoom> GetClassroomById(int id);
    }
}
