using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SliceMasterBE.Data;
using SliceMasterBE.Models;
using SliceMasterBE.Repositories;
using System.Security.Claims;

namespace SliceMasterBE.Controllers
{
	[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Roles ="User")]
	[Route("api/[controller]/[action]")]
	[ApiController]
	
	public class ItemsUserController : ControllerBase
	{
		private readonly IItemUserRepo _itemUser;

		public ItemsUserController(IItemUserRepo itemUser)
		{
			_itemUser = itemUser;
		}
		[HttpGet]
	
		public async Task<IActionResult> GetCartItems()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var items = await _itemUser.GetCartItems(userId);
			return Ok(items);
		}
		[HttpGet]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

		public async Task<IActionResult> GetCartOffers()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var items = await _itemUser.GetCartOffers(userId);
			return Ok(items);
		}
        [HttpPost]
		public async Task<IActionResult> AddItemToCart(Item item) {
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var res = await _itemUser.AddToItemCart(userId, item);
			if (res != null)
			{
				return Ok(res);
			}
			return BadRequest();
		}
		[HttpPost]
		public async Task<IActionResult> AddOfferToCart(Offer offer) {

			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var res = await _itemUser.AddToOfferCart(userId, offer);
			if (res != null)
			{
				return Ok(res);
			}
			return BadRequest();
		}
		[HttpPost]
		public async Task<IActionResult> DeleteItemFromCart(Item item)
		{

			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var res = await _itemUser.DeleteItemFromCart(userId, item);
			if (res != null)
			{
				return Ok(res);
			}
			return BadRequest();
		}
		[HttpPost]
		public async Task<IActionResult> DeleteOfferFromCart(Offer offer)
		{

			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var res = await _itemUser.DeleteOfferFromCart(userId, offer);
			if (res != null)
			{
				return Ok(res);
			}
			return BadRequest();
		}

		[HttpGet]
		public async Task<IActionResult> GetUserOrder()
		{

			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var Order = await _itemUser.GetUserOrder(userId);
			
			
				return Ok(Order);
			
		}
		[HttpPost]
		public async Task<IActionResult> Checkout(CheckoutModel check)
		{

			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var res = await _itemUser.Checkout(userId, check);
			if (res != null)
			{
				return Ok(res);
			}
			return BadRequest();
		}
		[HttpGet]
		public async Task<IActionResult> IsCheckedout()
		{

			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var res = await _itemUser.IsCheckedout(userId);
		
				return Ok(res);
		
			
		}
		[HttpGet]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> IsAdmin()
		{

			var role = User.FindFirstValue(ClaimTypes.Role);		
				return Ok(role);
		
			
		}
	}
}
