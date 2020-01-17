using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace VladyslavOrlovPromo.Web.NetCore.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(ILogger<ProfileController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Profile/Index action gets called");

            return View();
        }
    }
}