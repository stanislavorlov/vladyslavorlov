using System.Web.Mvc;
using VladyslavOrlovPromo.BusinessLogic;

namespace VladyslavOrlovPromo.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            ProfileLogic profileLogic = new ProfileLogic();

            ViewBag.rank = profileLogic.GetCurrentRanking();

            return View();
        }
    }
}