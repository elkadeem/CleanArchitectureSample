using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers
{
    public class VisitorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
