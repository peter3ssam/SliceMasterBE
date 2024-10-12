using System.Text.Json.Serialization;

namespace SliceMasterBE.Data
{
	public class Item
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Price { get; set; }
		public string ImgSrc { get; set; }
		public string? Category { get; set; }
		public string? Decription { get; set; }
		public int? OfferId { get; set; }
		[JsonIgnore]
		public Offer? Offer { get; set; } = null;
		[JsonIgnore]
		public List<Cart>? Cart { get; set; }

	}
}
