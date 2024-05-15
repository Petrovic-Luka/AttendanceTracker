using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace AttendanceTracker.DataAccess.JSON
{
    public class SubjectJSONRepository : ISubjectRepository
    {

        private readonly string _filePath;
        private readonly ILogger _logger;

        public SubjectJSONRepository(ILogger<SubjectJSONRepository> logger)
        {
            _filePath = Environment.CurrentDirectory;
            _filePath += "\\JsonFiles\\subjects.json";
            _logger = logger;
        }
        public async Task<List<Subject>> GetAllSubjects()
        {
            return await JsonHelper.ReadRecordsFromFile<Subject>(_filePath);
        }

        public async Task<Subject> GetSubjectById(int id)
        {
            var records= await JsonHelper.ReadRecordsFromFile<Subject>(_filePath);
            return records.FirstOrDefault(x => x.SubjectId == id);
        }
    }
}
