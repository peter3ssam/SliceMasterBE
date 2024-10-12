using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SliceMasterBE.Models;
using SliceMasterBE.Repositories;
using System.Security.Claims;

namespace SliceMasterBE.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IUserRepo _user;

		public AccountController(IUserRepo user)
        {
			_user = user;
	
		}
        [HttpPost]
		public async Task<IActionResult> Register([FromBody] SignupModel signup) { 
		
			var resault =	await _user.SignUp(signup,signup.Role);
			if (resault == true) {
				return Ok(resault);
			}
			return Unauthorized();
		}
		[HttpPost]
		public async Task<IActionResult> Login([FromBody] SigninModel signin) { 
		
			var resault =	await _user.SignIn(signin);
			//var userId = User.FindFirst
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (resault!=null) {
				return Ok(resault);
			}
			return Unauthorized();
		}
	
	}
}
