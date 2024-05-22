using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;
using AttendanceTracker.Hashing;
using Microsoft.Extensions.Logging;

namespace AttendanceTracker.DataAccess.JSON
{
    public class UserJSONRepository : IUserRepository
    {

        private readonly string _filePathProfessor;
        private readonly string _filePathStudent;
        private readonly ILogger _logger;

        public UserJSONRepository(ILogger<UserJSONRepository> logger)
        {
            _filePathProfessor = Environment.CurrentDirectory;
            _filePathProfessor += "\\JsonFiles\\professors.json";
            _filePathStudent = Environment.CurrentDirectory;
            _filePathStudent += "\\JsonFiles\\students.json";
            _logger = logger;
        }

        public async Task<Professor> GetProfessorById(int id)
        {
            var professors = await JsonHelper.ReadRecordsFromFile<Professor>(_filePathProfessor);
            return professors.FirstOrDefault(x => x.ProfessorId == id);
        }

        public async Task<Student> GetStudentByIndex(string index)
        {
            var students = await JsonHelper.ReadRecordsFromFile<Student>(_filePathStudent);
            return students.FirstOrDefault(x => x.Index == index);

        }

        public async Task<Professor> LogInProfessor(string email, string password)
        {
            var professors = await JsonHelper.ReadRecordsFromFile<Professor>(_filePathProfessor);
            return professors.FirstOrDefault(x => x.Email == email && x.Password == HashHelper.GetHash(password));
        }

        public async Task<Student> LogInStudent(string email, string password)
        {
            var students = await JsonHelper.ReadRecordsFromFile<Student>(_filePathStudent);
            return students.FirstOrDefault(x => x.Email == email && x.Password == HashHelper.GetHash(password));

        }


    }
}
