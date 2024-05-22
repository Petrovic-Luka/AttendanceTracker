using AttendanceTracker.BusinessLogic.Interfaces;
using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.DataAccess.JSON;
using AttendanceTracker.DataAccess.MongoDb;
using AttendanceTracker.DataAccess.SQL;
using AttendanceTracker.Domain;
using Microsoft.Extensions.Configuration;

namespace AttendanceTracker.BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        private IUserRepository _userRepository;
        IConfiguration _config;
        IServiceProvider _serviceProvider;

        public UserLogic(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _config = configuration;
            _serviceProvider = serviceProvider;
            SetUpDependency();
        }

        public async Task<Professor> LogInProfessor(string email, string password)
        {
            return await _userRepository.LogInProfessor(email, password);
        }

        public async Task<Student> LogInStudent(string Email, string Password)
        {
            return await _userRepository.LogInStudent(Email, Password);
        }

        private void SetUpDependency()
        {
            var dbInUse = _config.GetSection("DatabaseInUse").Value;
            switch (dbInUse)
            {
                case "SQL":
                    {
                        _userRepository = (IUserRepository)_serviceProvider.GetService(typeof(UserSQLRepository));
                        break;
                    }
                case "Mongo":
                    {
                        _userRepository = (IUserRepository)_serviceProvider.GetService(typeof(UserMongoRepository));
                        break;
                    }
                case "JSON":
                    {
                        _userRepository = (IUserRepository)_serviceProvider.GetService(typeof(UserJSONRepository));
                        break;
                    }
            }
        }
    }
}
