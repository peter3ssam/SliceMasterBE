using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SliceMasterBE.Data
{
	public class SliceMasterDB:IdentityDbContext<UserIdentity>
	{
        public SliceMasterDB(DbContextOptions<SliceMasterDB> dbOpt):base(dbOpt) 
        {
            
        }
        public DbSet<ContactMsg> Contact { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Checkout> Checkout { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=.;Database=SliceMaster;Integrated Security=true ;Encrypt=false ;TrustServerCertificate=True");
			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// Ensuring UserName is unique
			builder.Entity<UserIdentity>()
				.HasIndex(u => u.UserName)
				.IsUnique();

			// Ensuring Email is unique
			builder.Entity<UserIdentity>()
				.HasIndex(u => u.Email)
				.IsUnique();
		}

	}
}
