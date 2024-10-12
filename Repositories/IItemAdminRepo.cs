using SliceMasterBE.Data;
using SliceMasterBE.Models;

namespace SliceMasterBE.Repositories
{
	public interface IItemAdminRepo
	{
		Task<string> AddItem(ItemModel item);
		Task<string> AddOffer(OfferModel Offer);
		Task<string> DeleteItem(int id);
		Task<string> DeleteOffer(int id);
		Task<List<Item>> GetAllItems();
		Task<List<Offer>> GetAllOffers();
		Task<Item?> GetItem(int id);
		Task<Offer?> GetOffer(int id);
		Task<string> UpdateItem(int id, ItemModel item, int? OfferId = null);
		Task<string> UpdateOffer(Offer Offer);
		Task<string?> UpdateOrderStatues(Order order);
		Task<List<Order>> GetAllOrders();
	}
}