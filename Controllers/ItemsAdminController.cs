using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SliceMasterBE.Data;
using SliceMasterBE.Models;
using SliceMasterBE.Repositories;

namespace SliceMasterBE.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Roles = "Admin")]
	public class ItemsAdminController : ControllerBase
	{
		private readonly IItemAdminRepo _itemAdminRepo;

		public ItemsAdminController(IItemAdminRepo itemAdminRepo)
		{
			_itemAdminRepo = itemAdminRepo;
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> GetAllItems()
		{
			var items = await _itemAdminRepo.GetAllItems();
			return Ok(items);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetItem(int id)
		{
			var item = await _itemAdminRepo.GetItem(id);
			return Ok(item);
		}

		[HttpPost]
		public async Task<IActionResult> AddItem( ItemModel item)
		{
			await _itemAdminRepo.AddItem(item);
			return Created();
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateItem([FromRoute] int id, [FromBody] ItemModel item)
		{
			var res = await _itemAdminRepo.UpdateItem(id, item);
			if (res == null)
			{
				return BadRequest();
			}
			return Ok();
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteItem([FromRoute] int id)
		{
			var res = await _itemAdminRepo.DeleteItem(id);
			if(res == null)
			{
				return BadRequest();
			}
			return Ok(res);
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> GetAllOffers()
		{
			var items = await _itemAdminRepo.GetAllOffers();
			return Ok(items);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetOffer(int id)
		{
			var item = await _itemAdminRepo.GetOffer(id);
			return Ok(item);
		}

		[HttpPost]
		public async Task<IActionResult> AddOffer([FromBody] OfferModel Offer)
		{
			var res = await _itemAdminRepo.AddOffer(Offer);
			return Ok(res);
		}
		[HttpPut]
		public async Task<IActionResult> UpdateOffer([FromBody] Offer Offer)
		{
			var res = await _itemAdminRepo.UpdateOffer(Offer);
			if(res == null)
			{
				return BadRequest();
			}
			return Ok(res);
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteOffer([FromRoute] int id)
		{
			var res = await _itemAdminRepo.DeleteOffer(id);
			if (res == null)
			{
				return BadRequest();
			}
			return Ok(res);
		}
		[HttpPut]
		public async Task<IActionResult> UpdateOrderStatues(Order order)
		{
			var res = await _itemAdminRepo.UpdateOrderStatues(order);
			if (res == null)
			{
				return BadRequest();
			}
			return Ok(res);
		}
		[HttpGet]
		public async Task<IActionResult> GetAllOrders()
		{
		
			return Ok(await _itemAdminRepo.GetAllOrders());
		}

	}
}
