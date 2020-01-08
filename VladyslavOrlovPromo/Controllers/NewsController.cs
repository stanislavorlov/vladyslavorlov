using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using VladyslavOrlovPromo.DataAccess;

namespace VladyslavOrlovPromo.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            EntityDataModel model = new EntityDataModel();

            var posts = model.Posts.OrderByDescending(p => p.DateCreated).Skip(0).Take(5).ToList();

            var totalCount = model.Posts.Count();

            if (totalCount > posts.Count)
            {
                ViewBag.displayOlderPosts = true;
                ViewBag.nextPage = 2;
            }

            ViewBag.IsAdmin = IsAdmin();

            return View(posts);
        }

        public ActionResult Next(int page)
        {
            EntityDataModel model = new EntityDataModel();

            const int pageSize = 5;
            var skipCount = (page - 1) * pageSize;
            var posts = model.Posts.OrderByDescending(p => p.DateCreated).Skip(skipCount).Take(pageSize).ToList();

            var totalCount = model.Posts.Count();

            if (totalCount - skipCount > 5)
            {
                ViewBag.displayOlderPosts = true;
                ViewBag.nextPage = page + 1;
            }

            ViewBag.IsAdmin = IsAdmin();

            return View("Index", posts);
        }

        private bool IsAdmin()
        {
            if (Request.Cookies.AllKeys.Contains("user") && !string.IsNullOrEmpty(Request.Cookies["user"]?.Value))
            {
                var cookieValue = Request.Cookies["user"]?.Value;

                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                cookieValue = encoding.GetString(Convert.FromBase64String(cookieValue));

                int seperatorIndex = cookieValue.IndexOf(':');

                var cookieUsername = cookieValue.Substring(0, seperatorIndex);
                var cookiePassword = cookieValue.Substring(seperatorIndex + 1);

                var admin = ConfigurationManager.AppSettings["adminuser"].ToLower();
                var password = ConfigurationManager.AppSettings["adminpassword"].ToLower();

                if (String.Equals(cookieUsername, admin, StringComparison.CurrentCultureIgnoreCase) &&
                    String.Equals(cookiePassword, password, StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}