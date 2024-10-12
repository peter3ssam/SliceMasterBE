using Microsoft.AspNetCore.Identity;

namespace SliceMasterBE.Data
{
	public class UserIdentity : IdentityUser
	{
		//public int? OrderId { get; set; }
		public Order Order { get; set; }
	}
}
