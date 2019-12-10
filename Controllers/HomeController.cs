using System.Collections.Generic;
using System.Web.Mvc;
using VladyslavOrlovPromo.DataAccess;
using System.Linq;
using System.Runtime.Caching;

namespace VladyslavOrlovPromo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Slider()
        {
            var slideImages = MemoryCache.Default["slideImage"] as List<SlideImage>;

            if (slideImages == null)
            {
                slideImages = Session["slideImage"] as List<SlideImage>;
            }

            if (slideImages == null)
            {
                EntityDataModel entityDataModel = new EntityDataModel();
                slideImages = entityDataModel.SlideImages.OrderBy(si => si.OrderNumber).ToList();

                MemoryCache.Default["slideImage"] = slideImages;
                Session["slideImage"] = slideImages;
            }

            return PartialView(slideImages);
        }
    }
}