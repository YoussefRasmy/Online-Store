using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStoreBack_API.Data.Models
{
	public class Product
	{
		public int Id { get; set; }

		public string Name { get; set; }
		[ForeignKey("Category")]
		public int CategoryId { get; set; }
		[ForeignKey("Vendor")]
		public int VendorId { get; set; }
		public string Description { get; set; }
		public string ImagePath { get; set; }
		public float Quantity { get; set; }
		public float Price { get; set; }


		public virtual Category Category { get; set; }
		public virtual Vendor Vendor { get; set; }



	}
}
