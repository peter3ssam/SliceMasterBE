using SliceMasterBE.Data;
using SliceMasterBE.Models;

namespace SliceMasterBE.Repositories
{
	public interface IItemUserRepo
	{
		Task<List<Item>?> GetCartItems(string userId);
		Task<List<Offer>?> GetCartOffers(string userId);
		Task<string?> AddToItemCart(string userId, Item item);
		Task<string?> AddToOfferCart(string userId, Offer offer);
		Task<string?> DeleteOfferFromCart(string userId, Offer offer);
		Task<string?> DeleteItemFromCart(string userId, Item item);
		Task<string?> Checkout(string userId, CheckoutModel checkout);
		Task<string?> IsCheckedout(string userId);
		Task<Order> GetUserOrder(string userId);
	}
}