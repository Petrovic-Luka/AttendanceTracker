using AttendanceTracker.Domain;

namespace AttendanceTracker.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        public Task<Student> LogInStudent(string Email, string Password);
        public Task<Professor> LogInProfessor(string Email, string Password);
    }
}
