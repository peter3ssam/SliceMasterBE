using System.Text.Json.Serialization;

namespace SliceMasterBE.Data
{
	public class Order
	{
		public int Id { get; set; }
		public string OrderStatus { get; set; } = "";
		public int Total { get; set; } = 0;
		public string UserId { get; set; }
		[JsonIgnore]
		public Checkout? Chechout { get; set; }
		[JsonIgnore]
		public UserIdentity? User { get; set; }
		[JsonIgnore]
		public Cart? Cart { get; set; }
		
	}
}
