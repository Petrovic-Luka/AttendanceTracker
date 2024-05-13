using AttendanceTracker.DataAccess.Interfaces;
using AttendanceTracker.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("student")]
        public async Task<IActionResult> LogInStudent(LogInDTO request)
        {
            try
            {
                var output=await _userRepository.LogInStudent(request.MailAdress, request.Password);
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
                var output = await _userRepository.LogInProfessor(request.MailAdress, request.Password);
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
