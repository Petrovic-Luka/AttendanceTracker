using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;
using AttendanceTracker.Hashing;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace AttendanceTracker.DataAccess.SQL
{
    public class UserSQLRepository : IUserRepository
    {

        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        public UserSQLRepository(IConfiguration config, ILogger<UserSQLRepository> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task<Professor> GetProfessorById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("SqlConnection")))
            {

                try
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "Select [ProfessorId],[Email],[FullName],[Password] FROM [AttendanceTrackerDb].[dbo].[Professor] where [ProfessorId]=@Id";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", id);
                    var reader = await cmd.ExecuteReaderAsync();
                    if (reader.Read())
                    {
                        var professor = new Professor();
                        professor.ProfessorId = reader.GetInt32(0);
                        professor.Email = reader.GetString(1);
                        professor.FullName = reader.GetString(2);
                        professor.Password = reader.GetString(3);
                        return professor;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }
            }
        }

        public async Task<Student> GetStudentByIndex(string index)
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("SqlConnection")))
            {

                try
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "Select [StudentIndex],[Email],[FullName],[Password] FROM [AttendanceTrackerDb].[dbo].[Student] where [StudentIndex]=@Index";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Index", index);
                    var reader = await cmd.ExecuteReaderAsync();
                    if (reader.Read())
                    {
                        var student = new Student();
                        student.Index = reader.GetString(0);
                        student.Email = reader.GetString(1);
                        student.FullName = reader.GetString(2);
                        student.Password = reader.GetString(3);
                        return student;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }
            }
        }

        public async Task<Professor> LogInProfessor(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("SqlConnection")))
            {

                try
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "Select [ProfessorId],[Email],[FullName],[Password] FROM [AttendanceTrackerDb].[dbo].[Professor] where [Email]=@Email and [Password]=@Password";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", HashHelper.GetHash(password));
                    var reader = await cmd.ExecuteReaderAsync();
                    if (reader.Read())
                    {
                        var professor = new Professor();
                        professor.ProfessorId = reader.GetInt32(0);
                        professor.Email = reader.GetString(1);
                        professor.FullName = reader.GetString(2);
                        professor.Password = reader.GetString(3);
                        return professor;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }
            }
        }

        public async Task<Student> LogInStudent(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("SqlConnection")))
            {

                try
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();           
                    cmd.CommandText = "Select [StudentIndex],[Email],[FullName],[Password] FROM [AttendanceTrackerDb].[dbo].[Student] where [Email]=@Email and [Password]=@Password";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", HashHelper.GetHash(password));
                    var reader = await cmd.ExecuteReaderAsync();
                    if (reader.Read())
                    {
                        var student = new Student();
                        student.Index = reader.GetString(0);
                        student.Email=reader.GetString(1);
                        student.FullName=reader.GetString(2);
                        student.Password=reader.GetString(3);
                        return student;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }
            }
        }
    }
}
