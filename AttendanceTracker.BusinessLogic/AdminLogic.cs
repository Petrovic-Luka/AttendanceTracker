using AttendanceTracker.BusinessLogic.Interfaces;
using AttendanceTracker.DataAccess.JSON;
using AttendanceTracker.DataAccess.MongoDb;
using AttendanceTracker.DataAccess.SQL;

namespace AttendanceTracker.BusinessLogic
{
    public class AdminLogic : IAdminLogic
    {
        LessonSqlRepository lessonSql;
        LessonMongoRepository lessonMongo;
        LessonJSONRepository lessonJSON;
        public AdminLogic(LessonSqlRepository sql,LessonMongoRepository mongo, LessonJSONRepository json)
        {
            lessonSql=sql;
            lessonMongo=mongo;
            lessonJSON=json;
        }
        public async Task InsertLessonsMongoFromJSON()
        {
            await lessonMongo.AddFromOtherDb(await lessonJSON.GetAllLessons(), 0);
        }

        public async Task InsertLessonsSQLFromJSON()
        {
            await lessonSql.AddFromOtherDb(await lessonJSON.GetAllLessons(), 0);
        }

        public async Task SyncLessonsDatabases()
        {
            //sync mongo from sql
            var fromSql=await lessonSql.GetUnSyncedData();
            await lessonMongo.AddFromOtherDb(fromSql,1);
            await lessonSql.UpdateSyncFlags(fromSql.Select(x => x.LessonId).ToList());
            fromSql.Clear();
            var fromMongo=await lessonMongo.GetUnSyncedData();
            await lessonSql.AddFromOtherDb(fromMongo,1);
            await lessonMongo.UpdateSyncFlags(fromMongo.Select(x=>x.LessonId).ToList());
        }
    }
}
