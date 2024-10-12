namespace SliceMasterBE.Data
{
	public class Cart
	{
		public int Id { get; set; }
		public int OrderId { get; set; }
		public Order Order { get; set; }
		public List<Offer>? Offers { get; set; }
		public List<Item>? Item { get; set; }
	}
}
