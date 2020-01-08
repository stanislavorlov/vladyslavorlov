using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using VladyslavOrlovPromo.Models;

namespace VladyslavOrlovPromo.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ContactViewModel contact)
        {
            if (ModelState.IsValid)
            {
                SmtpClient client = new SmtpClient("mail.vladyslavorlov.com", 2525)
                {
                    EnableSsl = false,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential("service@vladyslavorlov.com", "Odcs90?7")
                };

                MailMessage mail = new MailMessage("service@vladyslavorlov.com", "Free.love.fun.art@gmail.com")
                {
                    Subject = "Question from WebSite",
                    Body = contact.Message + string.Format("\n\nBest regards,\n{0} {1}\n{2}", contact.FirstName, contact.LastName, contact.Email)
                };

                try
                {
                    client.Send(mail);
                    ViewBag.result = "Email was succesfully sent!";

                    ModelState.Clear();
                }
                catch (Exception)
                {
                    ViewBag.result = "Error occured during sending email";
                }

                return View();
            }

            return View(contact);
        }
    }
}