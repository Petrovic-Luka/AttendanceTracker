namespace AttendanceTracker.BusinessLogic.Interfaces
{
    public interface IAdminLogic
    {
        public Task SyncLessonsDatabases();
        public Task SyncAttendsDatabases();
        public Task InsertLessonsSQLFromJSON();
        public Task InsertLessonsMongoFromJSON();
        public Task InsertAttendsMongoFromJSON();
        public Task InsertAttendsSQLFromJSON();

        public Task ChangeDbInUse(string database);
    }
}
