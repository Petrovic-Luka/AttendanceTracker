using AttendanceTracker.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        IAdminLogic _logic;
        public AdminController(IAdminLogic logic) 
        {
            _logic=logic;
        }
        [HttpGet("SyncDbLessons")]
        public async Task<IActionResult> SyncDatabases()
        {
            try
            {
               Task.WaitAll(_logic.SyncLessonsDatabases());
               await _logic.SyncAttendsDatabases();
                return Ok("Databases synced");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("UpdateSqlFromJson")]
        public async Task<IActionResult> UpdateSqlFromJson()
        {
            try
            {
                await _logic.InsertLessonsSQLFromJSON();
                return Ok("Database synced");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("UpdateMongoFromJson")]
        public async Task<IActionResult> UpdateMongoFromJson()
        {
            try
            {
                await _logic.InsertLessonsMongoFromJSON();
                return Ok("Database synced");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SetDb")]
        public async Task<IActionResult> UpdateDatabaseInUse(string database)
        {
            try
            {
                await _logic.ChangeDbInUse(database);
                return Ok($"Database changed to {database}");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
