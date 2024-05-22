using AttendanceTracker.BusinessLogic.Interfaces;
using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.DataAccess.JSON;
using AttendanceTracker.DataAccess.MongoDb;
using AttendanceTracker.DataAccess.SQL;
using AttendanceTracker.Domain;
using Microsoft.Extensions.Configuration;

namespace AttendanceTracker.BusinessLogic
{
    public class SubjectLogic:ISubjectLogic
    {
        ISubjectRepository _subjectRepository;
        IConfiguration _config;
        IServiceProvider _serviceProvider;

        public SubjectLogic(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _config = configuration;
            _serviceProvider = serviceProvider;
            SetUpDependency();
        }

        public  async Task<List<Subject>> GetAllSubjects()
        {
            return await _subjectRepository.GetAllSubjects();
        }

        private void SetUpDependency()
        {
            var dbInUse = _config.GetSection("DatabaseInUse").Value;
            switch (dbInUse)
            {
                case "SQL":
                    {
                        _subjectRepository = (ISubjectRepository)_serviceProvider.GetService(typeof(SubjectSQLRepository));
                        break;
                    }
                case "Mongo":
                    {
                        _subjectRepository = (ISubjectRepository)_serviceProvider.GetService(typeof(SubjectMongoRepository));
                        break;
                    }
                case "JSON":
                    {
                        _subjectRepository = (ISubjectRepository)_serviceProvider.GetService(typeof(SubjectJSONRepository));
                        break;
                    }
            }
        }
    }
}
