using AttendanceTracker.BusinessLogic.Interfaces;
using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        IUserLogic _userLogic;

        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpPost("student")]
        public async Task<IActionResult> LogInStudent(LogInDTO request)
        {
            try
            {
                var output=await _userLogic.LogInStudent(request.MailAdress, request.Password);
                if(output!=null)
                {
                    return Ok(output);
                }
                else
                {
                    return BadRequest("User not found");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("professor")]
        public async Task<IActionResult> LogInProfessor(LogInDTO request)
        {
            try
            {
                var output = await _userLogic.LogInProfessor(request.MailAdress, request.Password);
                if (output != null)
                {
                    return Ok(output);
                }
                else
                {
                    return BadRequest("User not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
