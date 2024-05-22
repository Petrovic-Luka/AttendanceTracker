using AttendanceTracker.BusinessLogic.Interfaces;
using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.DataAccess.JSON;
using AttendanceTracker.DataAccess.MongoDb;
using AttendanceTracker.DataAccess.SQL;
using AttendanceTracker.Domain;
using Microsoft.Extensions.Configuration;

namespace AttendanceTracker.BusinessLogic
{
    public class LessonLogic : ILessonLogic
    {
        ILessonRepository _lessonRepository;
        IClassroomRepository _classroomRepository;
        IUserRepository _userRepository;
        ISubjectRepository _subjectRepository;
        IConfiguration _config;
        IServiceProvider _serviceProvider;

        public LessonLogic(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _config = configuration;
            _serviceProvider = serviceProvider;
            SetUpDependency();
        }

        public async Task<Guid> AddLesson(Lesson lesson)
        {
            var valid = await ValidateLesson(lesson);
            if (valid.IsValid == false)
            {
                throw new ArgumentException(valid?.ErrorMessage);
            }
            return await _lessonRepository.AddLesson(lesson);
        }

        public async Task DeleteLesson(Guid lessonId)
        {
            await _lessonRepository.DeleteLesson(lessonId);
        }

        public async Task<List<Lesson>> GetAllLessons()
        {
            return await _lessonRepository.GetAllLessons();
        }

        public async Task<Lesson> GetLessonById(Guid lessonId)
        {
            return await _lessonRepository.GetLessonById(lessonId);
        }

        public async Task UpdateLesson(Lesson lesson)
        {
            var valid = await ValidateLesson(lesson);
            if (valid.IsValid == false)
            {
                throw new ArgumentException(valid?.ErrorMessage);
            }
            await _lessonRepository.UpdateLesson(lesson);
        }

        private async Task<Result> ValidateLesson(Lesson lesson)
        {
            Result result = new Result();
            result.IsValid = true;
            var classRoom = await _classroomRepository.GetClassroomById(lesson.ClassRoomId);
            if (classRoom == null)
            {
                result.ErrorMessage += "ClassRoom not found" + Environment.NewLine;
                result.IsValid = false;
            }
            var subject = await _subjectRepository.GetSubjectById(lesson.SubjectId);
            if (subject == null)
            {
                result.ErrorMessage += "Subject not found" + Environment.NewLine;
                result.IsValid = false;
            }
            var professor = await _userRepository.GetProfessorById(lesson.ProfessorId);
            if (professor == null)
            {
                result.ErrorMessage += "Professor not found" + Environment.NewLine;
                result.IsValid = false;
            }
            return result;
        }
        private class Result
        {
            public bool IsValid;
            public string? ErrorMessage;
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
                        _classroomRepository = (IClassroomRepository)_serviceProvider.GetService(typeof(ClassroomSQLRepository));
                        _subjectRepository = (ISubjectRepository)_serviceProvider.GetService(typeof(SubjectSQLRepository));
                        break;
                    }
                case "Mongo":
                    {
                        _userRepository = (IUserRepository)_serviceProvider.GetService(typeof(UserMongoRepository));
                        _lessonRepository = (ILessonRepository)_serviceProvider.GetService(typeof(LessonMongoRepository));
                        _classroomRepository = (IClassroomRepository)_serviceProvider.GetService(typeof(ClassroomMongoRepository));
                        _subjectRepository = (ISubjectRepository)_serviceProvider.GetService(typeof(SubjectMongoRepository));
                        break;
                    }
                case "JSON":
                    {
                        _userRepository = (IUserRepository)_serviceProvider.GetService(typeof(UserJSONRepository));
                        _lessonRepository = (ILessonRepository)_serviceProvider.GetService(typeof(LessonJSONRepository));
                        _classroomRepository = (IClassroomRepository)_serviceProvider.GetService(typeof(ClassroomJSONRepository));
                        _subjectRepository = (ISubjectRepository)_serviceProvider.GetService(typeof(SubjectJSONRepository));
                        break;
                    }
            }
        }
    }
}
