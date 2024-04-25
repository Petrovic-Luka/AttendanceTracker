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

            //Domain models
            builder.Services.AddTransient<ILessonRepository, LessonJSONRepository>();
            builder.Services.AddTransient<ILessonLogic, LessonLogic>();
            builder.Services.AddSingleton<MongoDbConnection>();
            //MongoDb
        }
    }
}
