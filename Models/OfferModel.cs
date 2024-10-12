using SliceMasterBE.Data;
using System.Text.Json.Serialization;

namespace SliceMasterBE.Models
{
	public class OfferModel
	{
		public int? id { get; set; } = null;
		public string name { get; set; }
		public string imgSrc { get; set; }
		public int price { get; set; }
		public string offerLink { get; set; } = "menu/offers/";
		
		public List<Item>? items { get; set; } = null;
	}
}
