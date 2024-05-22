using AttendanceTracker.BusinessLogic.Interfaces;
using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.DataAccess.JSON;
using AttendanceTracker.DataAccess.MongoDb;
using AttendanceTracker.DataAccess.SQL;
using AttendanceTracker.Domain;
using Microsoft.Extensions.Configuration;

namespace AttendanceTracker.BusinessLogic
{
    public class AttendsLogic : IAttendsLogic
    {
        IAttendsRepository _attendsRepository;
        ILessonRepository _lessonRepository;
        IUserRepository _userRepository;
        IConfiguration _config;
        IServiceProvider _serviceProvider;


        public AttendsLogic(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _config = configuration;
            _serviceProvider = serviceProvider;
            SetUpDependency();
        }

        public async Task AddAttends(Attends attends)
        {
            //Data validation
            var lesson = await _lessonRepository.GetLessonById(attends.LessonId);
            if (lesson == null)
            {
                throw new ArgumentException("Lesson code not found");
            }
            var student = await _userRepository.GetStudentByIndex(attends.Index);
            if (student == null)
            {
                throw new ArgumentException("Student not found");
            }
            await _attendsRepository.AddAttends(attends);
        }

        public async Task<List<Attends>> GetAttendsByLesson(Guid lessonId)
        {
            return await _attendsRepository.GetAttendsByLesson(lessonId);
        }

        public async Task<List<Attends>> GetAttendsByStudent(string index)
        {
            return await _attendsRepository.GetAttendsByStudent(index);
        }

        private void SetUpDependency()
        {
            var dbInUse = _config.GetSection("DatabaseInUse").Value;
            switch (dbInUse)
            {
                case "SQL":
                    {
                        _userRepository = (IUserRepository)_serviceProvider.GetService(typeof(UserSQLRepository));
                        _lessonRepository = (ILessonRepository)_serviceProvider.GetService(typeof(LessonSqlRepository));
                        _attendsRepository = (IAttendsRepository)_serviceProvider.GetService(typeof(AttendsSQLRepository));
                        break;
                    }
                case "Mongo":
                    {
                        _userRepository = (IUserRepository)_serviceProvider.GetService(typeof(UserMongoRepository));
                        _lessonRepository = (ILessonRepository)_serviceProvider.GetService(typeof(LessonMongoRepository));
                        _attendsRepository = (IAttendsRepository)_serviceProvider.GetService(typeof(AttendsMongoRepository));
                        break;
                    }
                case "JSON":
                    {
                        _userRepository = (IUserRepository)_serviceProvider.GetService(typeof(UserJSONRepository));
                        _lessonRepository = (ILessonRepository)_serviceProvider.GetService(typeof(LessonJSONRepository));
                        _attendsRepository = (IAttendsRepository)_serviceProvider.GetService(typeof(AttendsJSONRepository));
                        break;
                    }
            }
        }
    }
}
