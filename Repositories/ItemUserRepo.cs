using Microsoft.EntityFrameworkCore;
using SliceMasterBE.Data;
using SliceMasterBE.Models;
using System;

namespace SliceMasterBE.Repositories
{
	public class ItemUserRepo : IItemUserRepo
	{
		private readonly SliceMasterDB _db;

		public ItemUserRepo(SliceMasterDB db)
		{
			_db = db;
		}
		public async Task<List<Item>?> GetCartItems(string userId)
		{

			var user = await _db.Users
					.Include(u => u.Order)
					.ThenInclude(o => o.Cart)
					.ThenInclude(c => c.Item)
					.FirstOrDefaultAsync(u => u.Id == userId);
			if (user == null)
			{
				return null;
			}
			
			return user.Order.Cart.Item;

		}		public async Task<List<Offer>?> GetCartOffers(string userId)
		{

			var user = await _db.Users
					.Include(u => u.Order)
					.ThenInclude(o => o.Cart)
					.ThenInclude(c => c.Offers)
					.FirstOrDefaultAsync(u => u.Id == userId);
			if (user == null)
			{
				return null;
			}
			
			return user.Order.Cart.Offers;

		}
		public async Task<string?> AddToItemCart(string userId, Item item)
		{

			var user = await _db.Users
					.Include(u => u.Order)
					.ThenInclude(o => o.Cart)
					.ThenInclude(c => c.Item)
					.FirstOrDefaultAsync(u => u.Id == userId);
			if (user == null)
			{
				return null;
			}
			user.Order.Cart.Item?.Add(item);
			var order = await _db.Orders.FindAsync(user.Order.Id);
			order.Total+=item.Price;
			await _db.SaveChangesAsync();
			return "the item added";

		}
		public async Task<string?> AddToOfferCart(string userId, Offer offer)
		{

			var user = await _db.Users
				.Include(u => u.Order)
				.ThenInclude(o => o.Cart)
				.ThenInclude(c => c.Offers)
				.FirstOrDefaultAsync(u => u.Id == userId);
			if (user == null)
			{
				return null;
			}
			user.Order.Cart.Offers?.Add(offer);
			var order = await _db.Orders.FindAsync(user.Order.Id);
			order.Total += offer.Price;
			await _db.SaveChangesAsync();
			return "the offer added";
		}
		public async Task<string?> DeleteItemFromCart(string userId, Item item)
		{
			// Load the user with the related entities
			var user = await _db.Users
				.Include(u => u.Order)
				.ThenInclude(o => o.Cart)
				.ThenInclude(c => c.Item)
				.FirstOrDefaultAsync(u => u.Id == userId);

			if (user == null)
			{
				return null;
			}
		
			// Check if Order, Cart, or Item is null
			if (user.Order.Cart.Item == null)
			{
				return "No items found in the user's cart.";
			}

			// Find the item in the cart by matching the ID
			var itemToRemove = user.Order.Cart.Item.FirstOrDefault(i => i.Id == item.Id);
			if (itemToRemove == null)
			{
				return "Item not found in cart.";
			}

			// Remove the item
			user.Order.Cart.Item.Remove(itemToRemove);
			var order = await _db.Orders.FindAsync(user.Order.Id);
			order.Total -= item.Price;
			await _db.SaveChangesAsync();

			return "The item was deleted.";
		}

		public async Task<string?> DeleteOfferFromCart(string userId, Offer offer)
		{

			// Load the user with the related entities
			var user = await _db.Users
				.Include(u => u.Order)
				.ThenInclude(o => o.Cart)
				.ThenInclude(c => c.Offers)
				.FirstOrDefaultAsync(u => u.Id == userId);

			if (user == null)
			{
				return null;
			}

			// Check if Order, Cart, or Item is null
			if (user.Order.Cart.Offers == null)
			{
				return "No Offers found in the user's cart.";
			}

			// Find the item in the cart by matching the ID
			var offerToRemove = user.Order.Cart.Offers.FirstOrDefault(i => i.Id == offer.Id);
			if (offerToRemove == null)
			{
				return "Offer not found in cart.";
			}

			// Remove the item
			user.Order.Cart.Offers.Remove(offerToRemove);
			var order = await _db.Orders.FindAsync(user.Order.Id);
			order.Total -= offer.Price;
			await _db.SaveChangesAsync();

			return "The Offer was deleted.";
		}
		public async Task<string?> Checkout(string userId,CheckoutModel checkout) { 
		
			var user = await _db.Users.Include(d=>d.Order).Where(e=>e.Id == userId).FirstAsync();
			if (user == null) { return null; }
			var order = await _db.Orders.FindAsync(user.Order.Id);
			Checkout check = new Checkout() { Address=checkout.Address,PaymentMethod=checkout.PaymentMethod,OrderId = order.Id,Order = order};
			await _db.Checkout.AddAsync(check);
			order.OrderStatus = "reqOrder";
			order.Chechout = check;
			await _db.SaveChangesAsync();
			return "Checkout Created.";
		}
		public async Task<string?> IsCheckedout(string userId)
		{

			var user = await _db.Users.Include(d => d.Order).Where(e => e.Id == userId).FirstAsync();
			if (user == null) { return null; }
			var orderId =  user.Order.Id;
			var getCheckoutData = await _db.Checkout.FirstOrDefaultAsync(d=>d.OrderId == orderId);
			if (getCheckoutData == null)
			{
				return null;
			}
			return "true";
		}
		public async Task<Order> GetUserOrder(string userId)
		{

			var user = await _db.Users.FindAsync(userId);
			if (user == null) { return null; }
			var order = await _db.Orders.Where(d => d.UserId == user.Id).Include(d=>d.Cart).FirstAsync();
			return order;
		}
	}
}
