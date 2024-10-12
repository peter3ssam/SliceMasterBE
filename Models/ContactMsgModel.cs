using System.ComponentModel.DataAnnotations;

namespace SliceMasterBE.Models
{
	public class ContactMsgModel
	{
		[Required]
        public string Name { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
