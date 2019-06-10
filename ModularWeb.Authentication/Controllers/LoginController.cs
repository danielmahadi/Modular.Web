using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModularWeb.Authentication.Models;
using ModularWeb.Authentication.Services;

namespace ModularWeb.Authentication.Controllers
{
    [Area("authentication")]
    public class LoginController
    {
        private readonly IdentityManagerService _service;

        public LoginController(IdentityManagerService service)
        {
            _service = service;
        }
        
        [HttpPost("api/login")]
        public async Task<ObjectResult> Login([FromBody] UserLogin userLogin)
        {
            try
            {
                var result = await _service.Login(userLogin.Username, userLogin.Password, userLogin.SaveLoginInfo);

                if (result.IsSuccess)
                {
                    return new OkObjectResult(result);
                }
                
                return new UnauthorizedObjectResult(null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPost("api/logout")]
        public async Task<ObjectResult> Logout()
        {
            try
            {
                var result = await _service.Logout();
                return new OkObjectResult(result);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}