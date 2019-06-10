using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ModularWeb.Authentication.Action;

namespace ModularWeb.Authentication.Services
{
    public class IdentityManagerService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityManagerService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public async Task CreateDefaultUser()
        {
            var defaultUser = new
            {
                Username = "admin",
                Email = "admin@telepathy.ai",
                Password = "Aa123456!"
            };
            
            var appUser = new ApplicationUser(defaultUser.Username, defaultUser.Email);

            await _userManager.CreateAsync(appUser, defaultUser.Password);
            await _signInManager.SignInAsync(appUser, isPersistent: false);
        }

        public async Task<LoginResult> Login(string username, string password, bool isPersistent)
        {
            var result = await _signInManager.PasswordSignInAsync(
                username, password, isPersistent, false);

            if (result.Succeeded)
            {
                return new LoginResult
                {
                    IsSuccess = true
                };
            }
            
            return new LoginResult
            {
                IsSuccess = false
            };
        }

        public async Task<LogOutResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
            
                return new LogOutResult
                {
                    IsSuccess = true,
                };
            }
            catch (Exception e)
            {
                Console.WriteLine("error when logout the user");
                Console.WriteLine(e);
                throw;
            }
        }
    }
    
    public class LoginResult
    {
        public bool IsSuccess { get; set; }
    }
    
    public class LogOutResult
    {
        public bool IsSuccess { get; set; }
    }
}