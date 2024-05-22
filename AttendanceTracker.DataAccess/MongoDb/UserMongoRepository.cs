using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;
using AttendanceTracker.Hashing;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;

namespace AttendanceTracker.DataAccess.MongoDb
{
    public class UserMongoRepository : IUserRepository
    {

        private readonly IMongoCollection<Student> _students;
        private readonly IMongoCollection<Professor> _professors;
        private readonly ILogger _logger;
        private readonly IClientSession _session;

        public UserMongoRepository(MongoDbConnection db, ILogger<UserMongoRepository> logger)
        {
            _students = db.StudentCollection;
            _professors = db.ProfessorCollection;
            _logger = logger;
            _session = db.Session;
        }

        public async Task<Professor> GetProfessorById(int id)
        {
            var results = await _professors.FindAsync(x => x.ProfessorId == id);
            return results.ToList().FirstOrDefault();
        }

        public async Task<Student> GetStudentByIndex(string index)
        {
            var results = await _students.FindAsync(x => x.Index == index);
            return results.ToList().FirstOrDefault();
        }

        public async Task<Professor> LogInProfessor(string email, string password)
        {
            try
            {
                var results = await _professors.FindAsync(x => x.Email == email && x.Password == HashHelper.GetHash(password));
                return results.ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task<Student> LogInStudent(string email, string password)
        {
            try
            {
                var results = await _students.FindAsync(x => x.Email == email && x.Password == HashHelper.GetHash(password));
                return results.ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
