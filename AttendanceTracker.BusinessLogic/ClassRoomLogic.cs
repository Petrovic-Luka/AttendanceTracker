using AttendanceTracker.BusinessLogic.Interfaces;
using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;

namespace AttendanceTracker.BusinessLogic
{
    public class ClassRoomLogic : IClassroomLogic
    {
        IClassroomRepository classroomRepository;

        public ClassRoomLogic(IClassroomRepository repository)
        {
            classroomRepository=repository;
        }
        public async Task<List<ClassRoom>> GetClassrooms()
        {
            return await classroomRepository.GetClassrooms();
        }
    }
}
