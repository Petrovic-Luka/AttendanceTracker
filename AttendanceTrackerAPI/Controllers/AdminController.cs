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
                await _logic.SyncLessonsDatabases();
                return Ok();
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
                return Ok();
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
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
