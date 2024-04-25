namespace AttendanceTracker.BusinessLogic.Interfaces
{
    public interface IAdminLogic
    {
        public Task SyncLessonsDatabases();
        public Task InsertLessonsSQLFromJSON();
        public Task InsertLessonsMongoFromJSON();
    }
}
