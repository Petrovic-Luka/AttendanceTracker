using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AttendanceTracker.DataAccess.SQL
{
    public class SubjectSQLRepository : ISubjectRepository
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        public SubjectSQLRepository(IConfiguration configuration, ILogger<SubjectSQLRepository> logger)
        {
            _config = configuration;
            _logger = logger;
        }

        public async Task<List<Subject>> GetAllSubjects()
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("SqlConnection")))
            {

                try
                {
                    var output = new List<Subject>();
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT [SubjectId],[Name] FROM [AttendanceTrackerDb].[dbo].[Subject]";
                    var reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        var subject = new Subject();
                        subject.SubjectId = reader.GetInt32(0);
                        subject.Name = reader.GetString(1);
                        output.Add(subject);
                    }
                    return output;
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
