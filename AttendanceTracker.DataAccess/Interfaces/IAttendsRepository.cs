using AttendanceTracker.Domain;

namespace AttendanceTracker.DataAccess.Interfaces
{
    public interface IAttendsRepository
    {
        public Task AddAttends(Attends attends);
        public Task<List<Attends>> GetAttendsByLesson(Guid lessonId);
        public Task<List<Attends>> GetAttendsByStudent(string index);
        public Task<List<Attends>> GetUnSyncedData();
        public Task AddFromOtherDb(List<Attends> attends,int synced);
        public Task UpdateSyncFlags(List<Guid> attendsIds);
    }
}
