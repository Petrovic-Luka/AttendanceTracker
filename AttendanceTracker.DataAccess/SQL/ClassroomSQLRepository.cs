using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AttendanceTracker.DataAccess.SQL
{
    public class ClassroomSQLRepository : IClassroomRepository
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        public ClassroomSQLRepository(IConfiguration configuration, ILogger<ClassroomSQLRepository> logger)
        {
            _config = configuration;
            _logger = logger;
        }

        public async Task<ClassRoom> GetClassroomById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                try
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT [ClassRoomId],[Name] FROM [AttendanceTrackerDb].[dbo].[ClassRoom] where ClassRoomId=@ClassRoomId";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ClassRoomId", id);
                    var reader = await cmd.ExecuteReaderAsync();
                    if (reader.Read())
                    {
                        var classroom = new ClassRoom();
                        classroom.ClassRoomId = reader.GetInt32(0);
                        classroom.Name = reader.GetString(1);
                        return classroom;
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

        public async Task<List<ClassRoom>> GetClassrooms()
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("SqlConnection")))
            {
                try
                {
                    var output = new List<ClassRoom>();
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT [ClassRoomId],[Name] FROM [AttendanceTrackerDb].[dbo].[ClassRoom]";
                    var reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        var classroom = new ClassRoom();
                        classroom.ClassRoomId = reader.GetInt32(0);
                        classroom.Name = reader.GetString(1);
                        output.Add(classroom);
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
