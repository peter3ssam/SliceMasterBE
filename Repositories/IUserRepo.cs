using Microsoft.AspNetCore.Identity;
using SliceMasterBE.Models;

namespace SliceMasterBE.Repositories
{
	public interface IUserRepo
	{
		Task<bool> SignUp(SignupModel signup, string role);
		Task<string> SignIn(SigninModel signin);
	}
}