using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SliceMasterBE.Data;
using SliceMasterBE.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SliceMasterBE.Repositories
{
	public class UserRepo : IUserRepo
	{
		private readonly UserManager<UserIdentity> _User;
		private readonly SignInManager<UserIdentity> _signinManager;
		private readonly IConfiguration _iconfig;
		private readonly SliceMasterDB _db;
		public UserRepo(UserManager<UserIdentity> User, SignInManager<UserIdentity> signinManager, IConfiguration iconfig,SliceMasterDB db)
		{
			_User = User;
			_signinManager = signinManager;
			_iconfig = iconfig;
			_db = db;
		}
		public async Task<bool> SignUp(SignupModel signup, string role)
		{
			var user = new UserIdentity()
			{

				UserName = signup.UserName,
				Email = signup.Email,
				EmailConfirmed = true,
				
				
			};
			var resault = await _User.CreateAsync(user, signup.Password);


			 
			if (resault.Succeeded)
			{
				
				

				 await _User.AddToRoleAsync(user, role);
			
				var mkOrder = new Order() { UserId = user.Id };
				 await _db.Orders.AddAsync(mkOrder);
				//await _db.SaveChangesAsync();
				var mkCart = new Cart() { OrderId = mkOrder.Id };
				 await _db.Carts.AddAsync(mkCart);
				user.Order = mkOrder;
				mkOrder.User = user;
				mkOrder.Cart = mkCart;
				mkCart.Order = mkOrder;
				await _db.SaveChangesAsync();
				//var r1 = mkOrder;
				return true;	
			}
			return false;
		}
		public async Task<string?> SignIn(SigninModel signin)
		{
			var result = await _signinManager.PasswordSignInAsync(signin.UserName, signin.Password, false, false);

			if (!result.Succeeded)
			{
				
				return null;
			}
			var user = await _User.FindByNameAsync(signin.UserName);
			var roles = await _User.GetRolesAsync(user);
			var role = roles.First();
			var authClaims = new List<Claim>
			{
				
				new Claim(ClaimTypes.Name, signin.UserName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.NameIdentifier,user.Id),
				new Claim(ClaimTypes.Role,role)
			};
			var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfig["JWT:Secret"]));

			var token = new JwtSecurityToken(
				issuer: _iconfig["JWT:ValidIssuer"],
				audience: _iconfig["JWT:ValidAudience"],
				expires: DateTime.UtcNow.AddDays(1),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
				);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
