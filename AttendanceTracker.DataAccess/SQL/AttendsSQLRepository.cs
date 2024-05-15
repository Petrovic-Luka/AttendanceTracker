using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AttendanceTracker.DataAccess.SQL
{
    public class AttendsSQLRepository : IAttendsRepository
    {
        //TODO add factory pattern for connection
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        public AttendsSQLRepository(IConfiguration config, ILogger<AttendsSQLRepository> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task AddAttends(Attends attends)
        {
            SqlTransaction transaction = null;
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("SqlConnection")))
            {

                try
                {
                    attends.AttendsId = Guid.NewGuid();
                    await connection.OpenAsync();
                    transaction = connection.BeginTransaction();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.Transaction = transaction;

                    cmd.CommandText = "Insert into Attends values (@AttendsId,@LessonId,@StudentIndex,0)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@LessonId", attends.LessonId);
                    cmd.Parameters.AddWithValue("@AttendsId", attends.AttendsId);
                    cmd.Parameters.AddWithValue("@StudentIndex", attends.Index);
                    var output = await cmd.ExecuteNonQueryAsync();
                    if (output == 0)
                    {
                        throw new ArgumentException("Insertion failed");
                    }
                    await transaction.CommitAsync();
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("Violation of UNIQUE KEY"))
                    {
                        transaction?.Rollback();
                        throw new ArgumentException("You have already marked your attendance.");
                    }
                }

                catch (Exception ex)
                {
                    transaction?.Rollback();
                    _logger.LogError(ex.Message);
                    throw;
                }
            }
        }

        public async Task<List<Attends>> GetAttendsByLesson(Guid lessonId)
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("SqlConnection")))
            {

                try
                {
                    var output = new List<Attends>();
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT[AttendsId],[LessonId],[StudentIndex],[Synced] FROM [AttendanceTrackerDb].[dbo].[Attends] where LessonId=@LessonId";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@LessonId", lessonId);
                    var reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        var attends = new Attends();
                        attends.AttendsId = Guid.Parse(reader.GetString(0));
                        attends.LessonId = Guid.Parse(reader.GetString(1));
                        attends.Index = reader.GetString(2);
                        attends.Synced = reader.GetBoolean(3);
                        output.Add(attends);                      
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

        public async Task<List<Attends>> GetAttendsByStudent(string index)
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("SqlConnection")))
            {

                try
                {
                    var output = new List<Attends>();
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT[AttendsId],[LessonId],[StudentIndex],[Synced] FROM [AttendanceTrackerDb].[dbo].[Attends] where StudentIndex=@StudentIndex";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@StudentIndex", index);
                    var reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        var attends = new Attends();
                        attends.AttendsId = Guid.Parse(reader.GetString(0));
                        attends.LessonId = Guid.Parse(reader.GetString(1));
                        attends.Index = reader.GetString(2);
                        attends.Synced = reader.GetBoolean(3);
                        output.Add(attends);
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
