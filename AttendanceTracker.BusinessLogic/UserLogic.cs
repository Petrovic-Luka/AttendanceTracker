using AttendanceTracker.BusinessLogic.Interfaces;
using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;

namespace AttendanceTracker.BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        private IUserRepository _userRepository;

        public UserLogic(IUserRepository repository)
        {
            _userRepository = repository;
        }

        public async Task<Professor> LogInProfessor(string email, string password)
        {
            return await _userRepository.LogInProfessor(email, password);
        }

        public async Task<Student> LogInStudent(string Email, string Password)
        {
            return await _userRepository.LogInStudent(Email, Password);
        }
    }
}
