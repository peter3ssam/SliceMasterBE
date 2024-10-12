using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SliceMasterBE.Data;
using SliceMasterBE.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IContactRepo,ContactRepo>();
builder.Services.AddTransient<IUserRepo, UserRepo>();
builder.Services.AddTransient<IItemAdminRepo, ItemAdminRepo>();
builder.Services.AddTransient<IItemUserRepo, ItemUserRepo>();
builder.Services.AddDbContext<SliceMasterDB>();



builder.Services.AddAuthentication(option =>
{
	option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
			   .AddJwtBearer(option =>
			   {
				   option.SaveToken = true;
				   option.RequireHttpsMetadata = false;
				   option.TokenValidationParameters = new TokenValidationParameters()
				   {
					   ValidateIssuer = true,
					   ValidateAudience = true,
					   ValidAudience = builder.Configuration["JWT:ValidAudience"],
					   ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
					   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
				   };
			   });

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("AdminPolicy", policy =>
		policy.RequireRole("Admin"));
});
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllOrigins",
		builder => builder
			.WithOrigins("http://192.168.1.7:4200")
			.AllowAnyMethod()
			.AllowAnyHeader());
});

builder.Services.AddIdentity<UserIdentity, IdentityRole>()
	.AddEntityFrameworkStores<SliceMasterDB>()
	.AddDefaultTokenProviders();
	
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}


// Seed roles and users
using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

	// Create Roles
	if (!await roleManager.RoleExistsAsync("Admin"))
{
	await roleManager.CreateAsync(new IdentityRole("Admin"));
}
if (!await roleManager.RoleExistsAsync("User"))
{
	await roleManager.CreateAsync(new IdentityRole("User"));
}

}



app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
