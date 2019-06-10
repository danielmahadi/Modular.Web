using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModularWeb.Authentication.Services;

namespace ModularWeb.Authentication.Controllers
{
    [Area("authentication")]
    [Route(("api/[controller]"))]
    public class AccountController : Controller
    {
        private readonly IdentityManagerService _service;

        public AccountController(IdentityManagerService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpPost("[action]")]
        public async Task<ObjectResult> CreateDefault()
        {
            try
            {

                await _service.CreateDefaultUser();
                return new OkObjectResult("created");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new BadRequestObjectResult(e);
            }
        }
    }
}