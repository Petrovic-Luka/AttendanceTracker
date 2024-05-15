using AttendanceTracker.BusinessLogic.Interfaces;
using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;

namespace AttendanceTracker.BusinessLogic
{
    public class AttendsLogic : IAttendsLogic
    {
        IAttendsRepository _attendsRepository;
        ILessonRepository _lessonRepository;
        IUserRepository _userRepository;

        public AttendsLogic(IAttendsRepository attendsRepo,ILessonRepository lessonsRepo, IUserRepository userRepository)
        {
            _attendsRepository = attendsRepo;
            _lessonRepository = lessonsRepo;
            _userRepository = userRepository;
        }

        public async Task AddAttends(Attends attends)
        {
            //Data validation
            var lesson = await _lessonRepository.GetLessonById(attends.LessonId);
            if (lesson == null)
            {
                throw new ArgumentException("Lesson code not found");
            }
            var student = _userRepository.GetStudentByIndex(attends.Index);
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
    }
}
