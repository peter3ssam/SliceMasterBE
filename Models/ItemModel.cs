using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SliceMasterBE.Models
{
	public class ItemModel
	{
		public int? Id { get; set; } = null;
		[Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
		public string ImgSrc { get; set; }
		public string? Decription { get; set; }
		[Required]
		public string Category { get; set; }
	}
}
