using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SliceMasterBE.Data;
using SliceMasterBE.Models;
using System;
using System.Linq;

namespace SliceMasterBE.Repositories
{
	public class ItemAdminRepo : IItemAdminRepo
	{
		private readonly SliceMasterDB _db;
		public ItemAdminRepo(SliceMasterDB db)
		{
			_db = db;
		}
		public async Task<List<Item>> GetAllItems()
		{
			return await _db.Items.ToListAsync();
		}
		public async Task<Item?> GetItem(int id)
		{
			return await _db.Items.FindAsync(id);
		}
		public async Task<string> AddItem(ItemModel item)
		{
			var newItem = new Item()
			{
				Name = item.Name,
				Price = item.Price,
				Category = item.Category,
				Decription = item.Decription,
				ImgSrc = item.ImgSrc,
				Offer = null
			};
			var res = await _db.Items.AddAsync(newItem);
			await _db.SaveChangesAsync();
			return res.ToString();
		}
		public async Task<string?> UpdateItem(int id, ItemModel item, int? OfferId = null)
		{
		
			var existingItem = await _db.Items.FindAsync(id);
			if (existingItem == null)
			{
				return null;
			}

			// Update the existing entity's properties
			existingItem.Name = item.Name;
			existingItem.Price = item.Price;
			existingItem.Category = item.Category;
			existingItem.Decription = item.Decription;
			existingItem.ImgSrc = item.ImgSrc;
			existingItem.OfferId = OfferId;

			await _db.SaveChangesAsync();
			return "Item updated successfully.";
		}
		public async Task<string?> DeleteItem(int id)
		{
			var existingItem = await _db.Items.FindAsync(id);
			if (existingItem == null)
			{
				return null;
			}

			_db.Items.Remove(existingItem);
			await _db.SaveChangesAsync();
			return "Item deleted successfully.";
		}


		public async Task<List<Offer>> GetAllOffers()
		{
			var r = await _db.Offers.Include(d => d.Item).ToListAsync();
			return r;
		}
		public async Task<Offer?> GetOffer(int id)
		{
			return await _db.Offers.Include(d => d.Item).FirstOrDefaultAsync(o => o.Id == id); ;
		}

		public async Task<string> AddOffer(OfferModel Offer)
		{
			// Create a new Offer entity
			var newOffer = new Offer()
			{
				Name = Offer.name,
				Price = Offer.price,
				ImgSrc = Offer.imgSrc,
				OfferLink = Offer.offerLink,
			};

			// Add the new Offer entity to the database
			var res = await _db.Offers.AddAsync(newOffer);
			await _db.SaveChangesAsync(); // Save changes to generate the Offer ID

			if (Offer.items != null)
			{
				foreach (var d in Offer.items)
				{
					// Find the existing Item by ID
					var item = await _db.Items.FindAsync(d.Id);

					if (item != null) // Ensure the item exists
					{
						item.OfferId = newOffer.Id; // Use the new Offer ID
					}
				}

				// Save changes to persist the updated items
				await _db.SaveChangesAsync();
			}

			return res.Entity.Id.ToString(); // Return the new Offer ID or another relevant property
		}

		public async Task<string?> UpdateOffer(Offer updatedOffer)
		{
		
			if (!await CheckExist(updatedOffer.Id, false))
			{
				return null;
			}

			
			var existingOffer = await _db.Offers
				.Include(o => o.Item) // Include related items
				.FirstOrDefaultAsync(o => o.Id == updatedOffer.Id);

			if (existingOffer == null)
			{
				return null;
			}

			
			foreach (var item in existingOffer.Item.ToList())
			{
				item.OfferId = null; 
			}

			// Update the offer properties
			existingOffer.Name = updatedOffer.Name;
			existingOffer.Price = updatedOffer.Price;
			existingOffer.ImgSrc = updatedOffer.ImgSrc;
			existingOffer.OfferLink = updatedOffer.OfferLink;

			// Retrieve the new set of items that need to be associated with the offer
			var newItems = await _db.Items
				.Where(item => updatedOffer.Item.Contains(item)) // Ensure only items related to the current offer are selected
				.ToListAsync();

			// Assign the OfferId to the new items
			foreach (var item in newItems)
			{
				item.OfferId = existingOffer.Id;
			}

			// Update the offer's items navigation property
			existingOffer.Item = newItems;

			// Save changes to the database
			await _db.SaveChangesAsync();

			return "succeeded";
		}

		public async Task<string?> DeleteOffer(int id)
		{
			if (!await CheckExist(id, false))
			{
				return null;
			}

			var offer = await GetOffer(id);
			var res = _db.Offers.Remove(offer);
			await _db.SaveChangesAsync();
			return res.ToString();
		}
		private async Task<bool> CheckExist(int id,bool item = true) {
			if (item)
			{

			if (await GetItem(id) == null)
			{
				return false;
			}
			return true;
			}
			else
			{
				if (await GetOffer(id) == null)
				{
					return false;
				}
				return true;
			}
		}
		public async Task<string?> UpdateOrderStatues(Order order)
		{
			var ord = await _db.Orders.FindAsync(order.Id);
			if (ord == null) { return null; }
			ord.OrderStatus = order.OrderStatus;
			await _db.SaveChangesAsync();
			return "order statues updated";
		}
		public async Task<List<Order>> GetAllOrders()
		{

			return await  _db.Orders.ToListAsync();
		}
	}
}
