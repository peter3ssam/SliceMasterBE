using System.ComponentModel.DataAnnotations;

namespace SliceMasterBE.Data
{
	public class ContactMsg
	{
        public int Id { get; set; }
        public string Name { get; set; }
		public string Email { get; set; }
		public string Message { get; set; }
	}
}
