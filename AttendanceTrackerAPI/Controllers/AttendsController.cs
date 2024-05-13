using AttendanceTracker.BusinessLogic.Interfaces;
using AttendanceTracker.Domain;
using AttendanceTracker.DTO;
using AttendanceTrackerAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttendsController : ControllerBase
    {
        IAttendsLogic _logic;
        public AttendsController(IAttendsLogic logic)
        {
            _logic = logic;
        }

        [HttpPost]
        public async Task<IActionResult> AddAttends(AttendsDTO attends)
        {
            try
            {
                await _logic.AddAttends(attends.ToAttendsFromDTO());
                return Ok("Attendance marked");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("lesson")]
        public async Task<IActionResult> GetAttendsByLesson(Guid lessonId)
        {
            try
            {
                return Ok(await _logic.GetAttendsByLesson(lessonId));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("student")]
        public async Task<IActionResult> GetAttendsByStudent(string index)
        {
            try
            {
                return Ok(await _logic.GetAttendsByStudent(index));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
