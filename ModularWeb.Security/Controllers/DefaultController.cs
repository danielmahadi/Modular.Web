using Microsoft.AspNetCore.Mvc;

namespace ModularWeb.Security.Controllers
{
    [Area("Security")]
    [Route("security/default")]
    public class DefaultController : Controller
    {
        [HttpGet("index")]
        public ViewResult Index()
        {
            return View();
        }

        [HttpGet("info")]
        public JsonResult Info()
        {
            return Json(new
            {
                Message = "Hello from ModularWeb.Security"
            });
        }
    }
}