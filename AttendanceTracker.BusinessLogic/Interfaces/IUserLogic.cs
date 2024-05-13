using AttendanceTracker.Domain;

namespace AttendanceTracker.BusinessLogic.Interfaces
{
    public interface IUserLogic
    {
        public Task<Student> LogInStudent(string Email, string Password);
        public Task<Professor> LogInProfessor(string Email, string Password);
    }
}
