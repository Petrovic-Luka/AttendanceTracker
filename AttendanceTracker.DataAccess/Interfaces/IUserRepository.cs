using AttendanceTracker.Domain;

namespace AttendanceTracker.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        public Task<Student> LogInStudent(string email, string password);
        public Task<Student> GetStudentByIndex(string index);
        public Task<Professor> LogInProfessor(string email, string password);
        public Task<Professor> GetProfessorById(int id);
    }
}
