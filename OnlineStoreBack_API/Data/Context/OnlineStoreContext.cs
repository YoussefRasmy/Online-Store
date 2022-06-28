using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineStoreBack_API.Data.Models;

namespace OnlineStoreBack_API.Data.Context
{

	public class OnlineStoreContext:IdentityDbContext<StoreUser>
	{
		public OnlineStoreContext(DbContextOptions<OnlineStoreContext>options):base(options)
		{

		}
		public DbSet<Cart> Carts { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductCart> CartProducts { get; set; }
		public DbSet<ProductOrder> OrderProducts { get; set; }
		public DbSet<Vendor> Vendors { get; set; }


		public DbSet<StoreUser> Users { get; set; } 
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<ProductCart>().HasKey(x => new { x.ProductId, x.CartId });
			builder.Entity<ProductOrder>().HasKey(x => new { x.ProductId, x.OrderId });
				

			base.OnModelCreating(builder);
		}

	}


}
	

