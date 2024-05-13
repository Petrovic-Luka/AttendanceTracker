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

        public async Task<Guid> AddLesson(Lesson lesson)
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

                    cmd.CommandText = "Insert into Lesson values (@LessonId,@ProfessorId,@SubjectId,@ClassRoomId,@Time,0)";
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

                    return lesson.LessonId;
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

        public async Task<List<Lesson>> GetUnSyncedData()
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("SqlConnection")))
            {

                try
                {
                    var output = new List<Lesson>();
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT[LessonId],[ProfessorId],[SubjectId],[ClassRoomId],[Time] FROM [AttendanceTrackerDb].[dbo].[Lesson] where Synced=0";
                    cmd.Parameters.Clear();
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
        public async Task UpdateSyncFlags(List<Guid> lessonIds)
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

                    foreach (var id in lessonIds)
                    {
                        cmd.CommandText = "update Lesson SET Synced=1 where LessonId=@LessonId";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@LessonId", id);
                        var output = await cmd.ExecuteNonQueryAsync();
                        if (output == 0)
                        {
                            throw new ArgumentException("Insertion failed");
                        }
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

                    cmd.CommandText = "update Lesson SET  ProfessorId=@ProfessorId,SubjectId=@SubjectId,ClassRoomId=@ClassRoomId,[Time]=@Time, [Synced]=0 where LessonId=@LessonId";
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

        public async Task AddFromOtherDb(List<Lesson> lessons,int synced)
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
                    foreach (var lesson in lessons)
                    {
                        cmd.CommandText = $"Insert into Lesson values (@LessonId,@ProfessorId,@SubjectId,@ClassRoomId,@Time,{synced})";
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
