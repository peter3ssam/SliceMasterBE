using SliceMasterBE.Models;
using System.Text.Json.Serialization;

namespace SliceMasterBE.Data
{
	public class Offer
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ImgSrc { get; set; }
		public string OfferLink { get; set; }
		public int Price { get; set; }
		//[JsonIgnore]
		public List<Item>? Item { get; set; }
		[JsonIgnore]
		public List<Cart>? Carts { get; set; }


}
}
