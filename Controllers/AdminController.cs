using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VladyslavOrlovPromo.Models;
using System.Linq;
using System.Runtime.Caching;
using VladyslavOrlovPromo.DataAccess;

namespace VladyslavOrlovPromo.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
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
                    EntityDataModel model = new EntityDataModel();
                    var slideImages = model.SlideImages.ToList();

                    return View("Editor", slideImages);
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(AdminLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var admin = ConfigurationManager.AppSettings["adminuser"].ToLower();
                var password = ConfigurationManager.AppSettings["adminpassword"].ToLower();

                if (model.Username.ToLower() == admin &&
                    model.Password.ToLower() == password)
                {
                    Response.SetCookie(new HttpCookie("user", Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                        .GetBytes(model.Username + ":" + model.Password))));

                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        public ActionResult New()
        {
            if (Request.Cookies.AllKeys.Contains("user") &&
                !string.IsNullOrEmpty(Request.Cookies["user"]?.Value))
            {
                var cookieValue = Request.Cookies["user"]?.Value;

                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                cookieValue =
                    encoding.GetString(Convert.FromBase64String(cookieValue));

                int seperatorIndex = cookieValue.IndexOf(':');

                var cookieUsername = cookieValue.Substring(0, seperatorIndex);
                var cookiePassword = cookieValue.Substring(seperatorIndex + 1);

                var admin = ConfigurationManager.AppSettings["adminuser"].ToLower();
                var password = ConfigurationManager.AppSettings["adminpassword"].ToLower();

                if (string.Equals(cookieUsername, admin, StringComparison.CurrentCultureIgnoreCase) &&
                    string.Equals(cookiePassword, password, StringComparison.CurrentCultureIgnoreCase))
                {
                    return View(new PostModel());
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult New(PostModel post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            EntityDataModel model = new EntityDataModel();
            if (post.Id == 0)
            {
                model.Posts.Add(new Post
                {
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    IsActive = true,
                    Title = post.Title,
                    Content = Encoding.ASCII.GetBytes(post.Body)
                });
                model.SaveChanges();
            }
            else
            {
                var p = model.Posts.Find(post.Id);

                if (p != null)
                {
                    p.DateModified = DateTime.UtcNow;
                    p.Title = post.Title;
                    p.Content = Encoding.ASCII.GetBytes(post.Body);

                    model.Entry(p).State = EntityState.Modified;
                    model.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            EntityDataModel model = new EntityDataModel();

            var post = model.Posts.Find(id);

            if (post != null)
            {
                return View("New", new PostModel
                {
                    Body = Encoding.ASCII.GetString(post.Content),
                    Id = post.Id,
                    Title = post.Title
                });
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            EntityDataModel model = new EntityDataModel();

            var post = model.Posts.Find(id);

            if (post != null)
            {
                model.Posts.Remove(post);
                model.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Editor(FormCollection formCollection, List<HttpPostedFileBase> files)
        {
            EntityDataModel model = new EntityDataModel();

            int idx = 0;
            foreach (var key in formCollection.AllKeys)
            {
                var id = int.Parse(key.Replace("title[", "").Replace("]", ""));
                var si = model.SlideImages.FirstOrDefault(s => s.Id == id);

                si.OrderNumber = idx + 1;

                if (files[idx] != null)
                {
                    si.ImageName = files[idx].FileName;
                    
                    using (var ms = new MemoryStream())
                    {
                        files[idx].InputStream.CopyTo(ms);
                        si.Image = ms.ToArray();
                    }
                }

                idx = idx + 1;

                si.Title = formCollection[key] ?? si.Title;
            }

            model.SaveChanges();

            MemoryCache.Default["slideImage"] = model.SlideImages.OrderBy(si => si.OrderNumber).ToList(); ;

            return RedirectToAction("Index");
        }

        public ActionResult NewSlide()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewSlide(string title, HttpPostedFileBase slideImage)
        {
            if (!string.IsNullOrEmpty(title) && slideImage != null)
            {
                EntityDataModel model = new EntityDataModel();

                SlideImage si = new SlideImage
                {
                    OrderNumber = 1,
                    ImageName = slideImage.FileName,
                    Title = title
                };

                using (var ms = new MemoryStream())
                {
                    slideImage.InputStream.CopyTo(ms);
                    si.Image = ms.ToArray();
                }

                var slideImages = model.SlideImages.ToList();

                foreach (var entity in slideImages)
                {
                    entity.OrderNumber = entity.OrderNumber + 1;
                }

                model.SlideImages.Add(si);
                model.SaveChanges();

                MemoryCache.Default["slideImage"] = model.SlideImages.OrderBy(s => s.OrderNumber).ToList(); ;
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteSlide(int id)
        {
            EntityDataModel model = new EntityDataModel();

            var slideImage = model.SlideImages.Find(id);

            if (slideImage != null)
            {
                model.SlideImages.Remove(slideImage);
                model.SaveChanges();

                MemoryCache.Default["slideImage"] = model.SlideImages.OrderBy(s => s.OrderNumber).ToList(); ;
            }

            return RedirectToAction("Index");
        }
    }
}