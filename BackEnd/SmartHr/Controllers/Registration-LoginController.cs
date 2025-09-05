using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart_HR___RMS.Dtos;

using Smart_HR___RMS.Repository;
using Smart_HR___RMS.Services;

namespace Smart_HR___RMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Registration_LoginController : ControllerBase
    {
        private readonly IUserLoginRegistrationInterface _userLogin;
        private readonly JwtService _jwtService;

        public Registration_LoginController(IUserLoginRegistrationInterface userLogin, JwtService jwtService)
        {
            _userLogin = userLogin;
            _jwtService = jwtService;
        }


        [HttpPost("Register")]
        public async Task<ActionResult<UserRegisters>> Register(UserDto request)
        {
            var user = await _userLogin.RegistrationAsync(request);
            if (user == null)
            {
                return BadRequest("User Is already Exists.");
            }
           
            return Ok(user);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(LoginDto login)
        {
            var user = await _userLogin.LoginAsync(login);
            if (user == null)
            {
                return BadRequest("Invalid email or password.");
            }

            var token = _jwtService.GenerateToken(user);
           
            return Ok(new { token,
           name = user.FirstName,
            Email = user.Email});
        }

        [Authorize(Roles = "HR")]
        [HttpGet("hr-only")]
        public IActionResult HrOnlyEndpoint()
        {
            return Ok("Only HR can access this.");
        }

        [Authorize(Roles = "Candidate")]
        [HttpGet("candidate-only")]
        public IActionResult CandidateOnlyEndpoint()
        {
            return Ok("Only Candidate can access this.");
        }
        [HttpGet("UserProfile")]
        public async Task<ActionResult> UserProfile()
        {
            var result = await _userLogin
                .UserProfile();
            return Ok(result);
        }
    }
}
