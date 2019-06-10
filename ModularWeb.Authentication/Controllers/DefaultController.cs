using Microsoft.AspNetCore.Mvc;

namespace ModularWeb.Authentication.Controllers
{
    [Area("authentication")]
    //[Route("authentication/default")]
    public class DefaultController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
        
        public JsonResult Info()
        {
            return Json(new
            {
                Message = "Hello from ModularWeb.Authentication"
            });
        }
    }
}