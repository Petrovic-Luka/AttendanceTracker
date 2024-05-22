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

        AttendsSQLRepository attendsSql;
        AttendsMongoRepository attendsMongo;
        AttendsJSONRepository attendsJSON;

        public AdminLogic(LessonSqlRepository lessonSql, LessonMongoRepository lessonMongo, LessonJSONRepository lessonJSON, AttendsSQLRepository attendsSql, AttendsMongoRepository attendsMongo, AttendsJSONRepository attendsJSON)
        {
            this.lessonSql = lessonSql;
            this.lessonMongo = lessonMongo;
            this.lessonJSON = lessonJSON;
            this.attendsSql = attendsSql;
            this.attendsMongo = attendsMongo;
            this.attendsJSON = attendsJSON;
        }

        //TODO setup db change endpoint             JsonHelper.AddOrUpdateAppSetting<string>("DatabaseInUse", "Mongo");

        public async Task InsertLessonsMongoFromJSON()
        {
            //TODO Add skip for already synced or delete file
            Task.WaitAll(lessonMongo.AddFromOtherDb(await lessonJSON.GetAllLessons(), 0));
            await attendsMongo.AddFromOtherDb(await attendsJSON.GetAllAttends(), 0);
            await lessonJSON.ClearFile();
            await attendsJSON.ClearFile();
        }

        public async Task InsertLessonsSQLFromJSON()
        {
            Task.WaitAll(lessonSql.AddFromOtherDb(await lessonJSON.GetAllLessons(), 0));
            await attendsSql.AddFromOtherDb(await attendsJSON.GetAllAttends(), 0);
            await lessonJSON.ClearFile();
            await attendsJSON.ClearFile();
        }

        public async Task SyncLessonsDatabases()
        {
            //sync mongo from sql
            var fromSql=await lessonSql.GetUnSyncedData();
            await lessonMongo.AddFromOtherDb(fromSql,1);
            await lessonSql.UpdateSyncFlags(fromSql.Select(x => x.LessonId).ToList());
            fromSql.Clear();
            //sync sql from mongo
            var fromMongo=await lessonMongo.GetUnSyncedData();
            await lessonSql.AddFromOtherDb(fromMongo,1);
            await lessonMongo.UpdateSyncFlags(fromMongo.Select(x=>x.LessonId).ToList());
        }

        public async Task SyncAttendsDatabases()
        {
            var fromSql = await attendsSql.GetUnSyncedData();
            await attendsMongo.AddFromOtherDb(fromSql, 1);
            await attendsSql.UpdateSyncFlags(fromSql.Select(x => x.AttendsId).ToList());
            fromSql.Clear();
            //sync sql from mongo
            var fromMongo = await attendsMongo.GetUnSyncedData();
            await attendsSql.AddFromOtherDb(fromMongo, 1);
            await attendsMongo.UpdateSyncFlags(fromMongo.Select(x => x.AttendsId).ToList());
        }

        public async Task InsertAttendsMongoFromJSON()
        {
            await attendsMongo.AddFromOtherDb(await attendsJSON.GetAllAttends(), 0);
        }

        public async Task InsertAttendsSQLFromJSON()
        {
            await attendsSql.AddFromOtherDb(await attendsJSON.GetAllAttends(), 0);
        }

        public Task ChangeDbInUse(string database)
        {
            if (database != "SQL" && database!="Mongo" && database!="JSON")
            {
                throw new ArgumentException("Bad input");
            }
            JsonHelper.ChangeDbInUse(database);
            return Task.CompletedTask;
        }
    }
}
