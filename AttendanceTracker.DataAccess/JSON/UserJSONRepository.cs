using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;

namespace AttendanceTracker.DataAccess.JSON
{
    public class UserJSONRepository : IUserRepository
    {
        public Task<Professor> LogInProfessor(string Email, string Password)
        {
            throw new NotImplementedException();
        }

        public Task<Student> LogInStudent(string Email, string Password)
        {
            throw new NotImplementedException();
        }
    }
}
