using AttendanceTracker.BusinessLogic;
using AttendanceTracker.BusinessLogic.Interfaces;
using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.DataAccess.JSON;
using AttendanceTracker.DataAccess.MongoDb;
using AttendanceTracker.DataAccess.SQL;

namespace AttendanceTrackerAPI
{
    public static class RegisterServices
    {
        /// <summary>
        /// Used to register all services required for dependency injection in app
        /// </summary>
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            //Configurating app level services
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            
            //BusinessLogic
            builder.Services.AddTransient<ILessonLogic, LessonLogic>();
            builder.Services.AddTransient<IAttendsLogic, AttendsLogic>();
            builder.Services.AddTransient<IAdminLogic, AdminLogic>();
            builder.Services.AddTransient<IUserLogic, UserLogic>();
            builder.Services.AddTransient<ISubjectLogic, SubjectLogic>();
            builder.Services.AddTransient<IClassroomLogic, ClassRoomLogic>();

            //admin part
            builder.Services.AddTransient<MongoDbConnection>();
            builder.Services.AddTransient<LessonSqlRepository>();
            builder.Services.AddTransient<LessonMongoRepository>();
            builder.Services.AddTransient<LessonJSONRepository>();


            var database = builder.Configuration["DatabaseInUse"];
            switch (database)
            {
                case "Mongo": ConfigureForMongoDb(builder); break;
                case "SQL": ConfigureForSQL(builder); break;
                case "JSON": ConfigureForJSON(builder); break;
            }

        }

        private static void ConfigureForSQL(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ILessonRepository, LessonSqlRepository>();
            builder.Services.AddTransient<IAttendsRepository, AttendsSQLRepository>();
            builder.Services.AddTransient<IUserRepository, UserSQLRepository>();
            builder.Services.AddTransient<ISubjectRepository, SubjectSQLRepository>();
            builder.Services.AddTransient<IClassroomRepository, ClassroomSQLRepository>();
        }
        private static void ConfigureForMongoDb(this WebApplicationBuilder builder)
        {         
            builder.Services.AddTransient<ILessonRepository, LessonMongoRepository>();
            builder.Services.AddTransient<IAttendsRepository, AttendsMongoRepository>();
            builder.Services.AddTransient<IUserRepository, UserMongoRepository>();
            builder.Services.AddTransient<ISubjectRepository, SubjectMongoRepository>();
            builder.Services.AddTransient<IClassroomRepository, ClassroomMongoRepository>();
        }
        private static void ConfigureForJSON(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ILessonRepository, LessonJSONRepository>();
            builder.Services.AddTransient<IAttendsRepository, AttendsJSONRepository>();
            builder.Services.AddTransient<IUserRepository, UserJSONRepository>();
            builder.Services.AddTransient<ISubjectRepository, SubjectJSONRepository>();
            builder.Services.AddTransient<IClassroomRepository, ClassroomJSONRepository>();
        }
    }
}
