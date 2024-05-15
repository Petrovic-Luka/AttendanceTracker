using AttendanceTracker.BusinessLogic.Interfaces;
using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;

namespace AttendanceTracker.BusinessLogic
{
    public class LessonLogic : ILessonLogic
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IClassroomRepository _classroomRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISubjectRepository _subjectRepository;

        public LessonLogic(ILessonRepository lessonRepository, IClassroomRepository classroomRepository, IUserRepository userRepository, ISubjectRepository subjectRepository)
        {
            _lessonRepository = lessonRepository;
            _classroomRepository = classroomRepository;
            _userRepository = userRepository;
            _subjectRepository = subjectRepository;
        }

        public async Task<Guid> AddLesson(Lesson lesson)
        {
            var valid = await ValidateLesson(lesson);
            if(valid.IsValid==false)
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
            await _lessonRepository.UpdateLesson(lesson);
        }

        private async Task<Result> ValidateLesson(Lesson lesson)
        {
            Result result= new Result();
            result.IsValid = true;
            var classRoom = await _classroomRepository.GetClassroomById(lesson.ClassRoomId);
            if (classRoom == null)
            {
                result.ErrorMessage += "ClassRoom not found";
                result.IsValid=false;
            }

            return result;
        }

        private class Result
        {
           public bool IsValid;
            public string? ErrorMessage;
        }
    }
}
