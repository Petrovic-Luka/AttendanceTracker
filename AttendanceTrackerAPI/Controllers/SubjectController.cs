using AttendanceTracker.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubjectController : ControllerBase
    {
        ISubjectLogic subjectLogic;

        public SubjectController(ISubjectLogic subjectLogic)
        {
            this.subjectLogic = subjectLogic;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            try
            {
                return Ok(await subjectLogic.GetAllSubjects());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
