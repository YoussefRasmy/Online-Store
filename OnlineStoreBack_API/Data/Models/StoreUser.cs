﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OnlineStoreBack_API.Data.Models
{
	public class StoreUser: IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string Mobile { get; set; }

		
		// navegation prop

		public virtual Cart Cart { get; set; }
		public virtual ICollection<Order> Orders { get; set; }

	}
}
