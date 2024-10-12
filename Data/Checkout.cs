using System.Text.Json.Serialization;

namespace SliceMasterBE.Data
{
	public class Checkout
	{
		public int Id{get;set;}
		public string Address{get;set;}
		public string PaymentMethod{get;set;}
		public int OrderId { get;set;}
		[JsonIgnore]
		public Order Order{get;set;}
	}
}
