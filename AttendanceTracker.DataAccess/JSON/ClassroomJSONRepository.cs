using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace AttendanceTracker.DataAccess.JSON
{
    public class ClassroomJSONRepository : IClassroomRepository
    {
        private readonly string _filePath;
        private readonly ILogger _logger;

        public ClassroomJSONRepository(ILogger<ClassroomJSONRepository> logger)
        {
            _filePath = Environment.CurrentDirectory;
            _filePath += "\\JsonFiles\\classrooms.json";
            _logger = logger;
        }

        public async Task<ClassRoom> GetClassroomById(int id)
        {
            var records= await JsonHelper.ReadRecordsFromFile<ClassRoom>(_filePath);
            return records.FirstOrDefault(x => x.ClassRoomId == id);
        }

        public async Task<List<ClassRoom>> GetClassrooms()
        {
            return await JsonHelper.ReadRecordsFromFile<ClassRoom>(_filePath);
        }

    }
}
