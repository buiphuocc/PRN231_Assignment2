using Microsoft.AspNetCore.Mvc;
using Services.CustomModels.Request;
using Services.Interfaces;
using System.Net;

namespace Assignment1.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IBranchAccountService _branchAccountService;

        public AuthenticationController(IBranchAccountService branchAccountService)
        {
            _branchAccountService = branchAccountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if(loginRequest == null)
            {
                return BadRequest("empty request");
            }
            try
            {
                var response = await _branchAccountService.AuthenticateAsync(loginRequest);
                return Ok(response);
            }catch(Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
