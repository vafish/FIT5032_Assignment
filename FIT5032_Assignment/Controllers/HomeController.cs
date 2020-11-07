using FIT5032_Assignment.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace FIT5032_Assignment.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [Authorize]
        public ActionResult SendEmail()
        {
            return View(new SendEmail());
        }

        public void sendEmails(SendEmail model, string toEmail)        
        {
            using (MailMessage mm = new MailMessage(model.Email, toEmail))
            {
                mm.Subject = model.Subject;
                mm.Body = model.Contents;
                if (model.Attachment != null) {
                    if (model.Attachment.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(model.Attachment.FileName);
                        mm.Attachments.Add(new Attachment(model.Attachment.InputStream, fileName));
                    }
                }
                
                mm.IsBodyHtml = false;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(model.Email, model.Password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;

                    try
                    {
                       smtp.Send(mm);
                        ViewBag.Message = "Email sent sucessfully!";
                    }
                   catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

          
        }

        [HttpPost]
        public ActionResult SendEmail(SendEmail model)
        {

            sendEmails(model, model.ToEmail);
            if (model.ToEmail2 != null)
                sendEmails(model, model.ToEmail2);
            if (model.ToEmail3 != null)
                sendEmails(model, model.ToEmail3);
            return View();
        }

    }

}