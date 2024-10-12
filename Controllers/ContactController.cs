using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SliceMasterBE.Data;
using SliceMasterBE.Models;
using SliceMasterBE.Repositories;

namespace SliceMasterBE.Controllers
{
	[Route("api/[controller]/[action]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles ="Admin")]
	[ApiController]
	public class ContactController : ControllerBase
	{
		private readonly IContactRepo _Contact;
	

		public ContactController(IContactRepo Contact)
        {
			_Contact = Contact;
        }
        [HttpGet]
		
		public async Task<IActionResult> getMessages(){
			var msgs = await _Contact.GetAllContactMsgs();
			return Ok(msgs);
		}
		[HttpGet("{id}")]
	
		public async Task<IActionResult> getMessage(int id){
			var msg = await _Contact.GetContactMsg(id);
			if(msg == null)
			{
				NoContent();
			}
			return Ok(msg);
		}
		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> CreateMessage([FromBody] ContactMsgModel msgModel){
			await _Contact.CreateContactMsg(msgModel);
			return Created();
		}
		[HttpDelete("{id}")]

		public async Task<IActionResult> DeleteMessage(int id){

			var res = await _Contact.DeleteContactMsg(id);
			if (res == null)
			{
				return BadRequest();
			}
			return Ok(res);
		}
	}
}
