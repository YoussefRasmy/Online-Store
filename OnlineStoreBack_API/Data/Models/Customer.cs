using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OnlineStoreBack_API.Data.Models
{
	public class Customer: IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string Mobile { get; set; }

		[ForeignKey("Cart")]
		public int CartId { get; set; }
		// navegation prop

		public virtual Cart Cart { get; set; }
		public virtual ICollection<Order> Orders { get; set; }

	}
}
