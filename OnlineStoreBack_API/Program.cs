using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using OnlineStoreBack_API.Data.Context;
using OnlineStoreBack_API.Data.Models;
using OnlineStoreBack_API.Repository;
using OnlineStoreBack_API.Repository.MabToDTO;
using OnlineStoreBack_API.Repository.Services;
using OnlineStoreBack_API.Repository.Services.User;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


#region Cors

var Policy = "Hamaada"; 
builder.Services.AddCors(options =>
{
	options.AddPolicy(Policy, builder =>
	{
		builder.AllowAnyOrigin();
		builder.AllowAnyMethod();
		builder.AllowAnyHeader();

	});
});
builder.Services.AddOptions();

#endregion

// Add services to the container.
#region Controllers and Swagger

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region Context

builder.Services.AddDbContext<OnlineStoreContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("OnlineStoreDB")));
builder.Services.AddHttpContextAccessor();
#endregion

#region ASP Identity

builder.Services.AddIdentity<StoreUser, IdentityRole>(options =>
{
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireLowercase = false;
	options.Password.RequiredLength = 5;
	options.Lockout.MaxFailedAccessAttempts = 3;
	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
	options.User.RequireUniqueEmail = true;//added new
	
}).AddEntityFrameworkStores<OnlineStoreContext>();
#endregion

#region Authentication

//when you validate token use the same key and the same hashing algorithm
builder.Services.AddAuthentication(optrions =>
{
	optrions.DefaultAuthenticateScheme = "default";
	optrions.DefaultChallengeScheme = "default";//so that he dont redirect the user in case he enterd un auth rout
})
	.AddJwtBearer("default", options =>
	{
		var secretKey = builder.Configuration.GetValue<string>("SecretKey");
		var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKey);
		var key = new SymmetricSecurityKey(secretKeyInBytes);
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateAudience = false,
			ValidateIssuer = false,
			IssuerSigningKey = key
		};
	});

#endregion

#region Authorization
// Customer , Admin
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("Admin", policy =>
	 policy.RequireClaim(ClaimTypes.Role, "Admin")
	);
	options.AddPolicy("Customer", policy =>
	policy.RequireClaim(ClaimTypes.Role, "Customer")
	);
});
#endregion

#region Services Regester
builder.Services.AddScoped<UserManager<StoreUser>, UserManager<StoreUser>>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductOrderRepository, ProductOrderRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IProductCartRepository,ProductCartRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IVendorRepository, VendorRepository>();
builder.Services.AddScoped<IProductToDTO, ProductToDTO>();

#endregion

var app = builder.Build();

#region Middlewares

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(
		Path.Combine(builder.Environment.ContentRootPath, "Assets")),
	RequestPath = "/Assets"
});

app.UseCors(Policy);

app.UseAuthentication();//

app.UseAuthorization();



app.MapControllers();
#endregion

app.Run();
