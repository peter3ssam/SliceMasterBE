using System.ComponentModel.DataAnnotations;

namespace SliceMasterBE.Models
{
	public class SigninModel
	{
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
