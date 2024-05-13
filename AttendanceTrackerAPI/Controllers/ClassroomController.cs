using AttendanceTracker.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassroomController : ControllerBase
    {
        IClassroomLogic classroomLogic;

        public ClassroomController(IClassroomLogic classroomLogic)
        {
            this.classroomLogic = classroomLogic;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClassRooms()
        {
            try
            {
                return Ok(await classroomLogic.GetClassrooms());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
