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
        }
        private static void ConfigureForMongoDb(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<MongoDbConnection>();
            builder.Services.AddTransient<ILessonRepository, LessonMongoRepository>();
        }
        private static void ConfigureForJSON(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ILessonRepository, LessonJSONRepository>();
        }
    }
}
