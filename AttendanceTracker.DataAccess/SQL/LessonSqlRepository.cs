using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AttendanceTracker.DataAccess.SQL
{
    public class LessonSqlRepository : ILessonRepository
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        public LessonSqlRepository(IConfiguration configuration, ILogger<LessonSqlRepository> logger)
        {
            _config = configuration;
            _logger = logger;
        }

        public async Task AddLesson(Lesson lesson)
        {
            SqlTransaction transaction = null;
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("SqlConnection")))
            {

                try
                {
                    lesson.LessonId = Guid.NewGuid();
                    await connection.OpenAsync();
                    transaction = connection.BeginTransaction();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.Transaction = transaction;              

                    cmd.CommandText = "Insert into Lesson values (@LessonId,@ProfessorId,@SubjectId,@ClassRoomId,@Time)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@LessonId", lesson.LessonId);
                    cmd.Parameters.AddWithValue("@ProfessorId", lesson.ProfessorId);
                    cmd.Parameters.AddWithValue("@SubjectId", lesson.SubjectId);
                    cmd.Parameters.AddWithValue("@ClassRoomId", lesson.ClassRoomId);
                    cmd.Parameters.AddWithValue("@Time", lesson.Time);
                    var output = await cmd.ExecuteNonQueryAsync();
                    if (output == 0)
                    {
                        throw new ArgumentException("Insertion failed");
                    }
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    _logger.LogError(ex.Message);
                    throw;
                }
            }
        }

        public async Task DeleteLesson(Guid lessonId)
        {
            SqlTransaction transaction = null;
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("SqlConnection")))
            {

                try
                {
                    await connection.OpenAsync();
                    transaction = connection.BeginTransaction();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.Transaction = transaction;

                    cmd.CommandText = "delete from Lesson where LessonId=@LessonId";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@LessonId", lessonId);
                    var output = await cmd.ExecuteNonQueryAsync();
                    if (output == 0)
                    {
                        throw new ArgumentException("Delete failed");
                    }
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    _logger.LogError(ex.Message);
                    throw;
                }
            }
        }

        public async Task<List<Lesson>> GetAllLessons()
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("SqlConnection")))
            {

                try
                {
                    var output = new List<Lesson>();
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT[LessonId],[ProfessorId],[SubjectId],[ClassRoomId],[Time] FROM [AttendanceTrackerDb].[dbo].[Lesson]";
                    var reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        var lesson = new Lesson();
                        lesson.LessonId = Guid.Parse(reader.GetString(0));
                        lesson.ProfessorId = reader.GetInt32(1);
                        lesson.SubjectId = reader.GetInt32(2);
                        lesson.ClassRoomId = reader.GetInt32(3);
                        lesson.Time = reader.GetDateTime(4);
                        output.Add(lesson);
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

        public async Task<Lesson> GetLessonById(Guid lessonId)
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("SqlConnection")))
            {

                try
                {
                    var output = new List<Lesson>();
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT[LessonId],[ProfessorId],[SubjectId],[ClassRoomId],[Time] FROM [AttendanceTrackerDb].[dbo].[Lesson] where LessonId=@LessonId";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@LessonId", lessonId);
                    var reader = await cmd.ExecuteReaderAsync();
                    if (reader.Read())
                    {
                        var lesson = new Lesson();
                        lesson.LessonId = Guid.Parse(reader.GetString(0));
                        lesson.ProfessorId = reader.GetInt32(1);
                        lesson.SubjectId = reader.GetInt32(2);
                        lesson.ClassRoomId = reader.GetInt32(3);
                        lesson.Time = reader.GetDateTime(4);
                        output.Add(lesson);
                        return lesson;
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

        public async Task UpdateLesson(Lesson lesson)
        {
            SqlTransaction transaction = null;
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("SqlConnection")))
            {

                try
                {
                    await connection.OpenAsync();
                    transaction = connection.BeginTransaction();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.Transaction = transaction;

                    cmd.CommandText = "update Lesson SET  ProfessorId=@ProfessorId,SubjectId=@SubjectId,ClassRoomId=@ClassRoomId,[Time]=@Time where LessonId=@LessonId";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@LessonId", lesson.LessonId);
                    cmd.Parameters.AddWithValue("@ProfessorId", lesson.ProfessorId);
                    cmd.Parameters.AddWithValue("@SubjectId", lesson.SubjectId);
                    cmd.Parameters.AddWithValue("@ClassRoomId", lesson.ClassRoomId);
                    cmd.Parameters.AddWithValue("@Time", lesson.Time);
                    var output = await cmd.ExecuteNonQueryAsync();
                    if (output == 0)
                    {
                        throw new ArgumentException("Insertion failed");
                    }
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    _logger.LogError(ex.Message);
                    throw;
                }
            }
        }
    }
}
