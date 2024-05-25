using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

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

                    cmd.CommandText = "Insert into Attends values (@AttendsId,@LessonId,@StudentIndex,0,0)";
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

        public async Task AddFromOtherDb(List<Attends> attends, int synced)
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
                    foreach (var attend in attends)
                    {
                        cmd.CommandText = "select Count(*) from Attends where AttendsId=@AttendsId";
                        cmd.Parameters.Clear();
                        //check if attends is already in db
                        cmd.Parameters.AddWithValue("@AttendsId", attend.AttendsId);
                        var count = await cmd.ExecuteScalarAsync();
                        if ((int)count > 0)
                        {
                            continue;
                        }
                        cmd.CommandText = $"Insert into Attends values (@AttendsId,@LessonId,@StudentIndex,{synced},@Deleted)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@LessonId", attend.LessonId);
                        cmd.Parameters.AddWithValue("@AttendsId", attend.AttendsId);
                        cmd.Parameters.AddWithValue("@StudentIndex", attend.Index);
                        cmd.Parameters.AddWithValue("@Deleted", attend.Deleted);
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

            public async Task<List<Attends>> GetAttendsByLesson(Guid lessonId)
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("SqlConnection")))
            {

                try
                {
                    var output = new List<Attends>();
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT [AttendsId],[LessonId],[StudentIndex],[Synced],[Deleted] FROM [AttendanceTrackerDb].[dbo].[Attends] where LessonId=@LessonId and [Deleted]=0";
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
                        attends.Deleted = reader.GetBoolean(4);
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
                    cmd.CommandText = "SELECT[AttendsId],[LessonId],[StudentIndex],[Synced],[Deleted] FROM [AttendanceTrackerDb].[dbo].[Attends] where [StudentIndex]=@StudentIndex and [Deleted]=0";
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
                        attends.Deleted = reader.GetBoolean(4);
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

        public async Task<List<Attends>> GetUnSyncedData()
        {
            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("SqlConnection")))
            {

                try
                {
                    var output = new List<Attends>();
                    await connection.OpenAsync();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT[AttendsId],[LessonId],[StudentIndex],[Synced],[Deleted] FROM [AttendanceTrackerDb].[dbo].[Attends] where [Synced]=0";
                    cmd.Parameters.Clear();
                    var reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        var attends = new Attends();
                        attends.AttendsId = Guid.Parse(reader.GetString(0));
                        attends.LessonId = Guid.Parse(reader.GetString(1));
                        attends.Index = reader.GetString(2);
                        attends.Synced = reader.GetBoolean(3);
                        attends.Deleted = reader.GetBoolean(4);
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

        public async Task UpdateSyncFlags(List<Guid> attendsIds)
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

                    foreach (var id in attendsIds)
                    {
                        cmd.CommandText = "update Attends SET [Synced]=1 where AttendsId=@AttendsId";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@AttendsId", id);
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
