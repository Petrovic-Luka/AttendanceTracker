using AttendanceTracker.BusinessLogic.Interfaces;
using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.DataAccess.JSON;
using AttendanceTracker.DataAccess.MongoDb;
using AttendanceTracker.DataAccess.SQL;
using AttendanceTracker.Domain;
using Microsoft.Extensions.Configuration;

namespace AttendanceTracker.BusinessLogic
{
    public class ClassRoomLogic : IClassroomLogic
    {
        IClassroomRepository classroomRepository;
        IConfiguration _config;
        IServiceProvider _serviceProvider;

        public ClassRoomLogic(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _config = configuration;
            _serviceProvider = serviceProvider;
            SetUpDependency();
        }
        public async Task<List<ClassRoom>> GetClassrooms()
        {
            return await classroomRepository.GetClassrooms();
        }

        private void SetUpDependency()
        {
            var dbInUse = _config.GetSection("DatabaseInUse").Value;
            switch (dbInUse)
            {
                case "SQL":
                    {
                        classroomRepository = (IClassroomRepository)_serviceProvider.GetService(typeof(ClassroomSQLRepository));
                        break;
                    }
                case "Mongo":
                    {
                        classroomRepository = (IClassroomRepository)_serviceProvider.GetService(typeof(ClassroomMongoRepository));
                        break;
                    }
                case "JSON":
                    {
                        classroomRepository = (IClassroomRepository)_serviceProvider.GetService(typeof(ClassroomJSONRepository));
                        break;
                    }
            }
        }
    }
}
