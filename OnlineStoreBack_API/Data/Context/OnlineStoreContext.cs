using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineStoreBack_API.Data.Models;

namespace OnlineStoreBack_API.Data.Context
{

	public class OnlineStoreContext:IdentityDbContext<Customer>
	{
		public OnlineStoreContext(DbContextOptions<OnlineStoreContext>options):base(options)
		{

		}
		 
		public DbSet<Customer> Customers { get; set; }
		public DbSet<CustomerMobile> CustomersMobile { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<VendorMobile>().HasKey(x => new { x.VendorId ,x.Mobile});
			builder.Entity<ProductCart>().HasKey(x => new { x.ProductId, x.CartId });
			builder.Entity<ProductOrder>().HasKey(x => new { x.ProductId, x.OrderId });
				

			builder.Entity<CustomerMobile>().HasKey(x => x.MobileNumber);
			base.OnModelCreating(builder);
		}

	}


}
	

