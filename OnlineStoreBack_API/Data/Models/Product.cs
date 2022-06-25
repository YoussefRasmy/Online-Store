using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStoreBack_API.Data.Models
{
	public class Product
	{
		
		public int Id { get; set; }
		[Required]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use English letters only please")]
		public string EnglishName { get; set; }
		[Required]
		//^[\u0621-\u064A0-9 ]+$
		//^[\u0621-\u064A\u0660-\u0669 ]+$
		[RegularExpression(@"^[\u0621-\u064A0-9 ]+$", ErrorMessage = "Use Arabic letters only please")]

		public string ArabicName { get; set; }
		[ForeignKey("Category")]
		public int CategoryId { get; set; }
		[ForeignKey("Vendor")]
		public int VendorId { get; set; }
		public string Description { get; set; }
		[Required]
		public string ImagePath { get; set; }
		[Required]
		[Range(1, 10000, ErrorMessage = "Can only be between 1 .. 10000")]
		public float Quantity { get; set; }
		[Required]
		[Range(1, 100000, ErrorMessage = "Can only be between 1 .. 100000")]
		public float Price { get; set; }

		//Nav Prop
		public virtual Category Category { get; set; }
		public virtual Vendor Vendor { get; set; }
		//public virtual ICollection<Cart> Carts { get; set; } Will Creat new Table 
		public virtual ICollection<ProductCart> ProductCarts { get; set; }

		public virtual ICollection<ProductOrder> MyProperty { get; set; }

	}
}
