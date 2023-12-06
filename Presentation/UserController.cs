using Microsoft.AspNetCore.Mvc;
using UserAPI.Application;
using UserAPI.Domain;

namespace UserAPI.Presentation
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("getDataUser")]
        public async Task<IActionResult> GetDataUser(string param)
        {
            var message = await _userRepository.GetUsersAsync(param);

            return Ok(new { User = message });
        }


        [HttpPut("setDataUser")]
        public async Task<IActionResult> SetDataUser([FromBody] User model)
        {
            try
            {
                var (status, message) = await _userRepository.SetUserAsync(model);

                if (status == 0)
                    return BadRequest(new { Message = message });
                return Ok(new { Message = message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("delDataUser")]
        public async Task<ActionResult> DelDataUser(int id)
        {
            var (status, message) = await _userRepository.DeleteUserAsync(id);
            if (status == 0)
                return BadRequest(new { Message = message });
            return Ok(new { Message = message });
        }


    }
}
