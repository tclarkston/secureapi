using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Models;
using System.Linq;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var token = _userService.Authenticate(model.Username, model.Password);

            if (token == null)
                return Unauthorized(new { message = "Username or password is incorrect" });

            return new JsonResult(token);
        }
    }
}
